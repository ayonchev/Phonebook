using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phonebook.Services
{
    public static class DataConstants
    {
        private const string DatabaseFilename = "phonebook.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                return Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        DatabaseFilename
                    );
            }
        }
    }
}
