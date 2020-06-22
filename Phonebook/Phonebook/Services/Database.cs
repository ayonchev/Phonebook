using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Services
{
    public class Database
    {
        static SQLiteAsyncConnection connection;

        public SQLiteAsyncConnection Connection => connection;

        public Database()
        {
            connection = new SQLiteAsyncConnection(DataConstants.DatabasePath, DataConstants.Flags);
        }

        public void Initialize(Type[] types)
        {
            using (var conn = new SQLiteConnection(DataConstants.DatabasePath, DataConstants.Flags))
            {
                conn.CreateTables(CreateFlags.None, types);
            }
        }

        public void Seed<T>(IEnumerable<T> items, int minimumCount)
            where T : BaseEntity, new()
        {
            using(var conn = new SQLiteConnection(DataConstants.DatabasePath, DataConstants.Flags))
            {
                int currentCount = conn.Table<T>().Count();

                if (currentCount < minimumCount)
                {
                    conn.InsertAll(items);
                }
            }
        }

        public async Task<List<T>> GetItems<T>()
            where T : BaseEntity, new()
        {
            var items = await connection.Table<T>().ToListAsync();
            return items;
        }

        public async Task<int> DeleteItem<T>(object primaryKey)
            where T : BaseEntity, new()
        {
            return await connection.DeleteAsync<T>(primaryKey);
        }

        public async Task SaveAsync<T>(T item)
            where T : BaseEntity, new()
        {
            if (item.Id == 0)
            {
                await connection.InsertAsync(item);
            }
            else
            {
                await connection.UpdateAsync(item);
            }
        }
    }
}
