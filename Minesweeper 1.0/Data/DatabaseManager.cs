using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;


namespace Minesweeper_1._0.Data
{
    internal class DatabaseManager
    {
        private string connectionString =
            ConfigurationManager.ConnectionStrings["MinesweeperDBConnectionString"].ConnectionString;

        public void SaveRecord(string playerName, int mineCount, int timeSeconds, bool isWin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Records 
                                (PlayerName, MineCount, TimeSeconds, DatePlayed, IsWin)
                                VALUES (@name, @mines, @time, @date, @win)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", playerName);
                    cmd.Parameters.AddWithValue("@mines", mineCount);
                    cmd.Parameters.AddWithValue("@time", timeSeconds);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@win", isWin);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable GetLeaderboard(string filter)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT TOP 10 PlayerName, MineCount, TimeSeconds, DatePlayed
                         FROM Records
                         WHERE IsWin = 1";

                if (filter == "This Week")
                    query += " AND DatePlayed >= DATEADD(WEEK, -1, GETDATE())";

                else if (filter == "This Month")
                    query += " AND DatePlayed >= DATEADD(MONTH, -1, GETDATE())";

                else if (filter == "This Year")
                    query += " AND DatePlayed >= DATEADD(YEAR, -1, GETDATE())";

                query += " ORDER BY TimeSeconds ASC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}