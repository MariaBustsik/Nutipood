using Nutipood.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nutipood.Data
{
    public class ArveDbContext
    {
        //Создаём базу данных счетов для хранения счетов, генерируемых приложением

        private const SQLite.SQLiteOpenFlags openFlags =
           //открытие базы данных для чтения и записи
           SQLite.SQLiteOpenFlags.ReadWrite |
           // создание базы данных, если она не существует
           SQLite.SQLiteOpenFlags.Create |
           //активировать доступ 
           SQLite.SQLiteOpenFlags.SharedCache;

        private readonly SQLiteAsyncConnection database;

        // конструктор dbcontext
        public ArveDbContext(string dbName)
        {
            // dbName представляет собой имя базы данных
            string baseDbPath = Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string dbPath = Path.Combine(baseDbPath, dbName);
            // создать базу данных, если она не существует, и установить соединение
            database = new SQLiteAsyncConnection(dbPath, openFlags);
            //создать таблицу, если она не существует
            var res = database.CreateTableAsync<Arve>().Result;


        }


        public Task<List<Arve>> GetAllAsync()
        {
            // Читать все счета
            return database.Table<Arve>().ToListAsync();
        }
        public Task<Arve> GetByIdAsync(int id)
        {
            // Чтение конкретного счета-фактуры с указанием его ID
            return database.Table<Arve>().Where(i => i.Id == id).FirstOrDefaultAsync();

            //использовать метод GetAsync вместо table.where
        }
        public Task<int> InsertAsync(Arve arve)
        {
            // вставить новый счет
            return database.InsertAsync(arve);
        }
        public Task<int> UpdateAsync(Arve arve)
        {
            //обновление существующего счета
            return database.UpdateAsync(arve);
        }
        public Task<int> DeleteAsync(Arve arve)
        {
            //удаляет существующий счет
            return database.DeleteAsync(arve);
        }

        public Task<int> DeleteAsyncById(int id)
        {
            //удаляет существующий счет-фактуру в соответствии с его ID
            return database.DeleteAsync(id);
        }


    }
}
