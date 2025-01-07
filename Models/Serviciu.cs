using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBellissima.Models
{
    public class Serviciu
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string DenumireServiciu { get; set; }
        public decimal Pret { get; set; }
        public int DurataMinute { get; set; }

        // Fk
        public int CategorieId { get; set; }

 
    }
}
