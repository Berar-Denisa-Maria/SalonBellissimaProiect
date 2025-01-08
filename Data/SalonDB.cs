using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonBellissima.Models;

namespace SalonBellissima.Data
{
    public class SalonDB
    {
        readonly SQLiteAsyncConnection _database;

        public SalonDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Categorie>().Wait();
            _database.CreateTableAsync<Serviciu>().Wait();
            _database.CreateTableAsync<Angajat>().Wait();
            _database.CreateTableAsync<Programare>().Wait();
            _database.CreateTableAsync<Recenzie>().Wait();
        }

        // Operatii pentru Categorie
        public Task<List<Categorie>> GetCategorieAsync()
        {
            return _database.Table<Categorie>().ToListAsync();
        }

        public Task<Categorie> GetCategorieAsync(int id)
        {
            return _database.Table<Categorie>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveCategorieAsync(Categorie clist)
        {
            if (clist.Id != 0)
                return _database.UpdateAsync(clist);
            else
                return _database.InsertAsync(clist);
        }

        public Task<int> DeleteCategorieAsync(Categorie clist)
        {
            return _database.DeleteAsync(clist);
        }

        // Operatii pentru Serviciu
        public Task<List<Serviciu>> GetServiciuAsync()
        {
            return _database.Table<Serviciu>().ToListAsync();
        }

        public Task<Serviciu> GetServiciuAsync(int id)
        {
            return _database.Table<Serviciu>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveServiciuAsync(Serviciu slist)
        {
            if (slist.Id != 0)
                return _database.UpdateAsync(slist);
            else
                return _database.InsertAsync(slist);
        }

        public Task<int> DeleteServiciuAsync(Serviciu slist)
        {
            return _database.DeleteAsync(slist);
        }

        public Task<List<Serviciu>> GetServiciiByCategorieIdAsync(int categorieId)
        {
            return _database.Table<Serviciu>()
                            .Where(s => s.CategorieId == categorieId)
                            .ToListAsync();
        }

        // Operatii pentru Angajat
        public Task<List<Angajat>> GetAngajatiAsync()
        {
            return _database.Table<Angajat>().ToListAsync();
        }

        public Task<int> SaveAngajatAsync(Angajat angajat)
        {
            if (angajat.Id != 0)
                return _database.UpdateAsync(angajat);
            else
                return _database.InsertAsync(angajat);
        }

        public Task<int> DeleteAngajatAsync(Angajat angajat)
        {
            return _database.DeleteAsync(angajat);
        }

        // Operatii pentru Programare
        public Task<List<Programare>> GetProgramariAsync()
        {
            return _database.Table<Programare>().ToListAsync();
        }

        public Task<int> SaveProgramareAsync(Programare programare)
        {
            if (programare.Id != 0)
                return _database.UpdateAsync(programare);
            else
                return _database.InsertAsync(programare);
        }

        public Task<int> DeleteProgramareAsync(Programare programare)
        {
            return _database.DeleteAsync(programare);
        }

        // Operatii pentru Recenzie
        public Task<List<Recenzie>> GetRecenziiAsync()
        {
            return _database.Table<Recenzie>().ToListAsync();
        }

        public Task<int> SaveRecenzieAsync(Recenzie recenzie)
        {
            return _database.InsertAsync(recenzie);
        }

        public Task<int> DeleteRecenzieAsync(Recenzie recenzie)
        {
            return _database.DeleteAsync(recenzie);
        }
    }
}
