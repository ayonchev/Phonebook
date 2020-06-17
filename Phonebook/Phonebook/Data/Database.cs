﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class Database
    {
        static SQLiteAsyncConnection connection;
        static bool initialized = false;

        public SQLiteAsyncConnection Connection => connection;

        public async static void Initialize(Type[] types)
        {
            if (!initialized)
            {
                connection = new SQLiteAsyncConnection(Constants.DatabasePath);

                foreach (var type in types)
                {
                    await connection.CreateTableAsync(type);
                }

                initialized = true;
            }
        }

        public static async void Seed<T>(IEnumerable<T> items, int minimumCount) 
            where T : BaseEntity, new()
        {
            int currentCount = await connection.Table<T>().CountAsync();

            if(currentCount < minimumCount)
            {
                await connection.InsertAllAsync(items);
            }
        }

        public async Task<List<T>> GetItems<T>() 
            where T : BaseEntity, new()
        {
            var items = await connection.Table<T>().ToListAsync();
            return items;
        }

        public async Task<int> DeleteItem<T>(T item) 
            where T : BaseEntity, new()
        {
            return await connection.DeleteAsync(item);
        }

        public async Task SaveAsync<T>(T item) 
            where T : BaseEntity, new()
        {
            if(item.Id == 0)
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