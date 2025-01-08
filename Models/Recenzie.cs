using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SalonBellissima.Models
{
    public class Recenzie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TextRecenzie { get; set; }

        public DateTime DataRecenzie { get; set; }
    }
}

