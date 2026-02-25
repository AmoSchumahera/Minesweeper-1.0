using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Minesweeper_1._0.Models;

namespace Minesweeper_1._0.Data
{
    public class RecordRepository : IRecordRepository
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MinesweeperDBConnectionString"].ConnectionString;

        public void Add(Record record)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Records
                                (PlayerName, MineCount, TimeSeconds, DatePlayed, IsWin)
                                VALUES (@name, @mines, @time, @date, @win)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", record.PlayerName);
                    cmd.Parameters.AddWithValue("@mines", record.MineCount);
                    cmd.Parameters.AddWithValue("@time", record.TimeSeconds);
                    cmd.Parameters.AddWithValue("@date", record.DatePlayed);
                    cmd.Parameters.AddWithValue("@win", record.IsWin);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Record> GetTopRecords(string filter)
        {
            List<Record> records = new List<Record>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT TOP 10 * FROM Records WHERE IsWin = 1";

                if (filter == "This Week")
                    query += " AND DatePlayed >= DATEADD(WEEK, -1, GETDATE())";
                else if (filter == "This Month")
                    query += " AND DatePlayed >= DATEADD(MONTH, -1, GETDATE())";
                else if (filter == "This Year")
                    query += " AND DatePlayed >= DATEADD(YEAR, -1, GETDATE())";

                query += " ORDER BY TimeSeconds ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new Record
                        {
                            Id = (int)reader["Id"],
                            PlayerName = reader["PlayerName"].ToString(),
                            MineCount = (int)reader["MineCount"],
                            TimeSeconds = (int)reader["TimeSeconds"],
                            DatePlayed = (DateTime)reader["DatePlayed"],
                            IsWin = (bool)reader["IsWin"]
                        });
                    }
                }
            }

            return records;
        }
    }
}
