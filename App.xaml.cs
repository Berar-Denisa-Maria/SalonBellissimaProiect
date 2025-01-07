using System;
using SalonBellissima.Data;
using System.IO;

namespace SalonBellissima
{
    public partial class App : Application
    {
        static SalonDB database;
            public static SalonDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new
               SalonDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SalonDB.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
