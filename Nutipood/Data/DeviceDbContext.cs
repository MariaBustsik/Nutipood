using Nutipood.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static Nutipood.Models.SmartDevice;
using System.Threading.Tasks;
using System.IO;

namespace Nutipood.Data
{
    public class DeviceDbContext
    {
        //Ссылка на изображение по умолчанию
        private static string url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNHZNiga4vZ97fE7yHMCNPoKSF06OH-XvnQ&usqp=CAU";

        private const SQLite.SQLiteOpenFlags openFlags =
           //открытие базы данных для чтения и записи
           SQLite.SQLiteOpenFlags.ReadWrite |
           // создание базы данных, если она не существует
           SQLite.SQLiteOpenFlags.Create |
           //активировать доступ 
           SQLite.SQLiteOpenFlags.SharedCache;

        private readonly SQLiteAsyncConnection database;

        // конструктор dbcontext
        public DeviceDbContext(string dbName)
        {
            // dbName представляет собой имя базы данных
            string baseDbPath = Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string dbPath = Path.Combine(baseDbPath, dbName);
            // создать базу данных, если она не существует, и установить соединение
            database = new SQLiteAsyncConnection(dbPath, openFlags);
            //создать таблицу, если она не существует
            var res = database.CreateTableAsync<SmartDevice>().Result;
            if (res == CreateTableResult.Created)
                PeopledataBase();

        }

        private async void PeopledataBase()
        {
            try
            {
                SmartDevice device;
                device = new SmartDevice()
                {
                    Model = "PadFone Infinity2",
                    Tootja = "Asus",
                    Type = e_type.Nutitelefon,
                    Platvorm = e_platform.Android,
                    Hind = 129.00,
                    ImageUrl = url
                    
                    
                };
                // вставка объекта в базу данных
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Model = "Flare S8 Deluxe",
                    Tootja = "Cherry Mobile",
                    Type = e_type.Nutitelefon,
                    Platvorm = e_platform.Android,
                    Hind = 160.00,
                    ImageUrl = url

                };
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Model = "Wildfire E3",
                    Tootja = "HTC",
                    Type = e_type.Nutitelefon,
                    Platvorm = e_platform.Android,
                    Hind = 229.00,
                    ImageUrl = url


                };
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Model = "Yoga Tab 13",
                    Tootja = "Lenovo",
                    Type = e_type.Tahvelarvuti,
                    Platvorm = e_platform.Android,
                    Hind = 250.00,
                    ImageUrl = url

                };
                await database.InsertAsync(device);


                device = new SmartDevice()
                {
                    Model = "iPhone 13 Pro",
                    Tootja = "Apple",
                    Type = e_type.Nutitelefon,
                    Platvorm = e_platform.iOS,
                    Hind = 729.00,
                    ImageUrl = url


                };
                await database.InsertAsync(device);

                device = new SmartDevice()
                {
                    Model = "Galaxy Watch 4",
                    Tootja = "Samsung",
                    Type = e_type.Smart_watch,
                    Platvorm = e_platform.Android,
                    Hind = 112.00,
                    ImageUrl = url

                };
                await database.InsertAsync(device);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }



        public Task<List<SmartDevice>> GetAllAsync()
        {
            // Читать все устройства SmartDevices
            return database.Table<SmartDevice>().ToListAsync();
        }
        public Task<SmartDevice> GetByIdAsync(int id)
        {
            // Чтение конкретного SmartDevice с указанием его ID
            return database.Table<SmartDevice>().Where(i => i.Id == id).FirstOrDefaultAsync();

            //использовать метод GetAsync вместо table.where
        }
        public Task<int> InsertAsync(SmartDevice SmartDevice)
        {
            // вставить новое устройство SmartDevice
            return database.InsertAsync(SmartDevice);
        }
        public Task<int> UpdateAsync(SmartDevice SmartDevice)
        {
            //обновление существующего устройства SmartDevice
            return database.UpdateAsync(SmartDevice);
        }
        public Task<int> DeleteAsync(SmartDevice SmartDevice)
        {
            //удаляет существующее устройство SmartDevice
            return database.DeleteAsync(SmartDevice);
        }

        public Task<int> DeleteAsyncById(int id)
        {
            //удаляет существующее устройство SmartDevice в соответствии с его ID
            return database.DeleteAsync(id);
        }
    }
}
