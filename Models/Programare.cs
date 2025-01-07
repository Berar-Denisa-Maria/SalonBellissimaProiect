using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SalonBellissima.Models
{
    public class Programare
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ServiciuId { get; set; }

        public DateTime DataOra { get; set; }
    }
}
