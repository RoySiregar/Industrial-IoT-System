using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MQTTnet;
using MQTTnet.Client;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;

namespace IndustrialIoTLogger
{
    class Program
    {
        // Konfigurasi MQTT & Database
        static string _mqttServer = "127.0.0.1";
        static string _topic = "kafka/log/data"; // Topik baru
        static string _dbConnString = "Server=127.0.0.1;Port=3306;Database=aoi;Uid=root;Pwd=;"; // Sesuaikan DB Anda

        static async Task Main(string[] args)
        {
            Console.Title = "=== KAFKA LOG SIMULATOR & SAVER ===";

            // 1. JALANKAN SUBSCRIBER (PENERIMA & PENYIMPAN KE DB)
            _ = Task.Run(async () => await JalankanSubscriber());

            // 2. JALANKAN SIMULATOR (PENGIRIM DATA PALSU)
            await JalankanRobotSimulator();
        }

        // ==========================================
        // BAGIAN 1: SUBSCRIBER (SIMPAN KE DB)
        // ==========================================
        static async Task JalankanSubscriber()
        {
            var mqttFactory = new MqttFactory();
            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttServer, 1883)
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                try
                {
                    // Deserialisasi ke Model Baru
                    var data = JsonConvert.DeserializeObject<KafkaLogData>(payload);
                    if (data != null) await SimpanKeDatabase(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR PARSE]: {ex.Message}");
                }
            };

            await mqttClient.ConnectAsync(mqttOptions);
            await mqttClient.SubscribeAsync(_topic);
            Console.WriteLine($"[SUBSCRIBER] Mendengarkan topik: {_topic}...\n");
            await Task.Delay(-1);
        }

        static async Task SimpanKeDatabase(KafkaLogData data)
        {
            using (var conn = new MySqlConnection(_dbConnString))
            {
                string sql = @"INSERT INTO kafka_log_data_temp 
                               (Factory, Floor, Line, Project, StationName, EquipmentName, CurrentTime, 
                                Level, Category, CategoryNumber, ErrorCode, Description, Message, 
                                CreatedAt, TestResult, ISN, GroupName, FixtureResult)
                               VALUES 
                               (@Factory, @Floor, @Line, @Project, @StationName, @EquipmentName, @CurrentTime, 
                                @Level, @Category, @CategoryNumber, @ErrorCode, @Description, @Message, 
                                @CreatedAt, @TestResult, @ISN, @GroupName, @FixtureResult)";

                try
                {
                    await conn.ExecuteAsync(sql, data);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[DB SAVED] {data.StationName} | {data.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[DB ERROR] {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        // ==========================================
        // BAGIAN 2: SIMULATOR (DATA RANDOM SESUAI FILE JSON)
        // ==========================================
        static async Task JalankanRobotSimulator()
        {
            var factory = new MqttFactory();
            using var client = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder().WithTcpServer(_mqttServer, 1883).Build();
            await client.ConnectAsync(options);

            var rnd = new Random();

            // DATA REFERENSI DARI JSON ANDA
            var projects = new[] {
                new { Proj = "Olympus", Line = "2F19", Floor = "2F" },
                new { Proj = "4332", Line = "2F13", Floor = "2F" },
                new { Proj = "3896LG", Line = "3F13", Floor = "3F" }
            };

            var stations = new[] { "R1", "R2", "R3", "R4", "R5", "R6", "R7" };
            var groups = new[] { "WIFI", "BOARD", "NFT", "FwUpdate", "Telephone", "Final", "LEDCheck", "PMAC", "BATT" };

            while (true)
            {
                // 1. Pilih Project & Line secara acak
                var selectedProj = projects[rnd.Next(projects.Length)];
                string selectedStation = stations[rnd.Next(stations.Length)];
                string selectedGroup = groups[rnd.Next(groups.Length)];

                // 2. Tentukan Skenario (Executing / Completion / Close Device)
                int scenario = rnd.Next(1, 4); // 1, 2, atau 3

                var log = new KafkaLogData
                {
                    Factory = "F1",
                    Floor = selectedProj.Floor,
                    Line = selectedProj.Line,
                    Project = selectedProj.Proj,
                    StationName = selectedStation,
                    EquipmentName = "",
                    CurrentTime = DateTime.Now,
                    CreatedAt = DateTime.Now.AddMilliseconds(200), // CreatedAt biasanya sedikit lebih lambat
                    Level = "TRACE",
                    Category = "FIXTURE",
                    CategoryNumber = rnd.Next(1, 25).ToString(),
                    ErrorCode = "000000"
                };

                if (scenario == 1) // SCENARIO: EXECUTING (DUT Placed)
                {
                    string isn = "KSMB" + rnd.Next(100000, 999999) + rnd.Next(1000, 9999); // Random ISN
                    log.Description = "EXECUTING";
                    log.Message = $"<1-{isn}> DUT Placed ";
                    log.ISN = isn;
                    log.GroupName = selectedGroup;
                    log.TestResult = null;
                    log.FixtureResult = "";
                }
                else if (scenario == 2) // SCENARIO: COMPLETION (Test Pass/Fail)
                {
                    bool isPass = rnd.NextDouble() > 0.1; // 90% Pass
                    log.Description = "COMPLETION";
                    log.Message = isPass ? "<4-P> Test Pass" : "<4-F> Test Fail";
                    log.ISN = "";
                    log.GroupName = selectedGroup; // Terkadang kosong di JSON, tapi kita isi
                    log.TestResult = isPass ? "PASS" : "FAIL";
                    log.FixtureResult = "";
                }
                else // SCENARIO: Close Device (Fixture Result)
                {
                    log.Description = "";
                    log.Message = $"close test device #{log.CategoryNumber} success";
                    log.ISN = "";
                    log.GroupName = selectedGroup;
                    log.TestResult = null;
                    log.FixtureResult = "PASS";
                }

                // Kirim Data
                string json = JsonConvert.SerializeObject(log);
                await client.PublishAsync(new MqttApplicationMessageBuilder()
                    .WithTopic(_topic)
                    .WithPayload(json)
                    .Build());

                Console.WriteLine($"[SIMULATOR] Kirim data {log.StationName} - {log.Description}...");

                await Task.Delay(1000); // Kirim setiap 1 detik
            }
        }
    }
}