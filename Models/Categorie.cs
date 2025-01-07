using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBellissima.Models
{
    public class Categorie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), Unique]
        public string DenumireCategorie { get; set; }

        [Ignore]
        public List<Serviciu> Servicii { get; set; }
    }
}
