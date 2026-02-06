using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using Newtonsoft.Json.Linq;
using MQTTnet;
using MQTTnet.Client;
using MySql.Data.MySqlClient;

namespace StationRunningStateService.Libs
{
    public class MQTTClient
    {
        private IMqttClient _client;
        private MqttClientOptions _options;
        private string _connString = "Server=10.176.33.76;Port=3306;Database=aoi;Uid=aoi;Pwd=aoi#123456;";

        // Antrean untuk menampung data sementara di memori
        private ConcurrentQueue<JObject> _dataQueue = new ConcurrentQueue<JObject>();
        private System.Timers.Timer _batchTimer;

        public MQTTClient(string server, int port, string user, string pass)
        {
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(server, port)
                .WithCredentials(user, pass)
                .Build();

            // Setel Timer untuk proses Batch Insert setiap 5 detik
            _batchTimer = new System.Timers.Timer(5000);
            _batchTimer.Elapsed += async (sender, e) => await ProcessBatchInsert();
            _batchTimer.AutoReset = true;
            _batchTimer.Enabled = true;
        }

        public async Task StartAsync(string topic)
        {
            _client.ApplicationMessageReceivedAsync += e =>
            {
                try
                {
                    string payloadText = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var data = JObject.Parse(payloadText);

                    // FILTERING: Hanya Factory F1 dan Floor 1F
                    if (data["Factory"]?.ToString() == "F1" && data["Floor"]?.ToString() == "1F")
                    {
                        // Masukkan ke antrean memori, bukan langsung ke DB
                        _dataQueue.Enqueue(data);

                        // Log tipis di terminal agar kita tahu data mengalir
                        if (_dataQueue.Count % 100 == 0)
                            Console.WriteLine($"[Antrean] Terkumpul {_dataQueue.Count} data...");
                    }
                }
                catch { /* Abaikan jika format JSON rusak */ }
                return Task.CompletedTask;
            };

            _client.ConnectedAsync += e => {
                Console.WriteLine("[OK] Terhubung ke MQTT. Sistem Batch aktif (5 detik).");
                return Task.CompletedTask;
            };

            await _client.ConnectAsync(_options);
            await _client.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter(f => f.WithTopic(topic)).Build());
        }

        private async Task ProcessBatchInsert()
        {
            if (_dataQueue.IsEmpty) return;

            List<JObject> itemsToInsert = new List<JObject>();
            while (_dataQueue.TryDequeue(out var item))
            {
                itemsToInsert.Add(item);
                if (itemsToInsert.Count >= 500) break; // Batasi maksimal 500 data per kiriman agar tidak kepanjangan
            }

            if (itemsToInsert.Count == 0) return;

            try
            {
                using (var conn = new MySqlConnection(_connString))
                {
                    await conn.OpenAsync();

                    // Kita gunakan Transaction agar lebih cepat dan aman
                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        string sql = @"INSERT INTO ptbproductiondata 
                            (Factory, Floor, Line, Project, StationName, EquipmentName, CurrentTime, Level, Category, Description, Message, SN, result, ct_time) 
                            VALUES 
                            (@Factory, @Floor, @Line, @Project, @StationName, @EquipmentName, @CurrentTime, @Level, @Category, @Description, @Message, @SN, @Result, @CT)";

                        foreach (var data in itemsToInsert) 
                        { 
                            string status = data["Desc"]?.ToString();
                            string messageText = data["Message"]?.ToString();

                            string resultFinal = null;
                            string ctValue = null;

                            // 1. LOGIKA PARSING CYCLE TIME & RESULT
                            // Sesuai contohmu, data ini muncul saat Desc adalah "COMPLETETION"
                            if (status == "COMPLETETION") 
                            { 
                                // Default untuk status COMPLETETION biasanya dianggap PASS 
                                // Kecuali ada logika lain dari mesin untuk FAIL
                                resultFinal = "PASS";   

                                if (!string.IsNullOrEmpty(messageText))
                                {
                                    // Melakukan parsing: "00:14,12620,10" -> diambil "00:14"
                                    string[] parts = messageText.Split(',');
                                    if (parts.Length > 0)
                                    {
                                        string rawCT = parts[0].Trim(); // Hasil: "00:14"

                                        // Jika ingin menyimpan hanya detiknya saja (14):
                                        if (rawCT.Contains(":"))
                                        {
                                            string[] timeParts = rawCT.Split(':');
                                            ctValue = timeParts[timeParts.Length - 1]; // Mengambil "14"
                                        }
                                        else
                                        {
                                            ctValue = rawCT;
                                        }
                                    }
                                }
                            }

                            using (var cmd = new MySqlCommand(sql, conn, transaction))
                            {
                                // Parameter standar tetap sama
                                cmd.Parameters.AddWithValue("@Factory", data["Factory"]?.ToString());
                                cmd.Parameters.AddWithValue("@Floor", data["Floor"]?.ToString());
                                cmd.Parameters.AddWithValue("@Line", data["Line"]?.ToString());
                                cmd.Parameters.AddWithValue("@Project", data["Project"]?.ToString());
                                cmd.Parameters.AddWithValue("@StationName", data["StationName"]?.ToString());
                                cmd.Parameters.AddWithValue("@EquipmentName", data["EquipmentName"]?.ToString());
                                cmd.Parameters.AddWithValue("@CurrentTime", data["CurrentTime"]?.ToString());
                                cmd.Parameters.AddWithValue("@Level", data["Level"]?.ToString());
                                cmd.Parameters.AddWithValue("@Category", data["Category"]?.ToString());
                                cmd.Parameters.AddWithValue("@Description", status);
                                cmd.Parameters.AddWithValue("@Message", messageText);
                                cmd.Parameters.AddWithValue("@SN", data["SN"]?.ToString());
                                    
                                // Parameter Hasil Parsing Baru
                                cmd.Parameters.AddWithValue("@Result", (object)resultFinal ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CT", (object)ctValue ?? DBNull.Value);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        await transaction.CommitAsync();
                    }
                }
                Console.WriteLine($"[DB] Berhasil Batch Insert {itemsToInsert.Count} data ke MySQL.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB BATCH ERROR] {ex.Message}");
            }
        }
    }
}