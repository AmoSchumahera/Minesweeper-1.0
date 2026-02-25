using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper_1._0.Models;

namespace Minesweeper_1._0.Data
{
    public interface IRecordRepository
    {
        void Add(Record record);
        List<Record> GetTopRecords(string filter);
    }
}
