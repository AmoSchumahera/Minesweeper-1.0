using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Minesweeper_1._0.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int MineCount { get; set; }
        public int TimeSeconds { get; set; }
        public DateTime DatePlayed { get; set; }
        public bool IsWin { get; set; }
    }
}
