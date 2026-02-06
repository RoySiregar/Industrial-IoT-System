using MySql.Data.MySqlClient;
using Dapper;
using StationRunningStateService.Model;

namespace StationRunningStateService.Libs
{
    public class MySQLHelper
    {
        private readonly string _connString;

        public MySQLHelper(string connString)
        {
            _connString = connString;
        }

        public async Task SaveToDatabase(MQTTMessage msg)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);

                // Fix format waktu: ganti titik (.) menjadi titik dua (:)
                string cleanTime = msg.CurrentTime.Replace(".", ":");

                // Tentukan PASS/FAIL dari level
                string res = msg.Level == "ERROR" ? "FAIL" : "PASS";

                // Ambil CT Time jika kategori ROBOT (biasanya angka pertama sebelum koma)
                string? ctValue = null;
                if (msg.Category == "ROBOT" && !string.IsNullOrEmpty(msg.Message))
                {
                    ctValue = msg.Message.Split(',')[0];
                }

                string sql = @"INSERT INTO ptbproductiondata 
                    (Factory, Floor, Line, Project, StationName, EquipmentName, CurrentTime, 
                     Level, Category, Description, Message, ct_time, result) 
                    VALUES 
                    (@Factory, @Floor, @Line, @Project, @StationName, @EquipmentName, @CurrentTime, 
                     @Level, @Category, @Desc, @Message, @ct_time, @result)";

                await conn.ExecuteAsync(sql, new
                {
                    msg.Factory,
                    msg.Floor,
                    msg.Line,
                    msg.Project,
                    msg.StationName,
                    msg.EquipmentName,
                    CurrentTime = cleanTime,
                    msg.Level,
                    msg.Category,
                    msg.Desc,
                    msg.Message,
                    ct_time = ctValue,
                    result = res
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MySQL Error]: {ex.Message}");
            }
        }
    }
}