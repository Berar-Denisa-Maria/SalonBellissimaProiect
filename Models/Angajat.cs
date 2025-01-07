using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBellissima.Models
{
    public class Angajat
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Functie { get; set; }
    }
}
