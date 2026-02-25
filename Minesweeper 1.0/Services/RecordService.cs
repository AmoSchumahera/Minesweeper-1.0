using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper_1._0.Models;
using Minesweeper_1._0.Data;

namespace Minesweeper_1._0.Services
{
    public class RecordService
    {
        private readonly IRecordRepository repository;

        public RecordService()
        {
            repository = new RecordRepository();
        }

        public void SaveGame(string playerName, int mines, int time, bool isWin)
        {
            Record record = new Record
            {
                PlayerName = playerName,
                MineCount = mines,
                TimeSeconds = time,
                DatePlayed = DateTime.Now,
                IsWin = isWin
            };

            repository.Add(record);
        }

        public List<Record> GetLeaderboard(string filter)
        {
            return repository.GetTopRecords(filter);
        }
    }
}
