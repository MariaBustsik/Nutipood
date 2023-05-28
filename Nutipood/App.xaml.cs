using Nutipood.Data;
using Nutipood.Models;
using Nutipood.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nutipood
{
    public partial class App : Application
    {
        //объявление корзины в App, чтобы ее было легче вызывать в других моделях представления
        public static Ostukorv _ostukorv;

        // объявление dbContext в качестве статической переменной для упрощения вызова в других моделях представления
        private static DeviceDbContext deviceDatabase;
        private static ArveDbContext arveDatabase;


        //Методы доступа
        public static Ostukorv Ostukorv
        {
            get { return _ostukorv; }
        }
        public static DeviceDbContext DeviceDatabase
        {
            get { return deviceDatabase; }
        }
        public static ArveDbContext ArveDatabase
        {
            get { return arveDatabase; }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            // инициализировать dbcontext SmartDevice
            string databaseName1 = "DbSmartDevice.db3";
            deviceDatabase = new DeviceDbContext(databaseName1);

            // инициализировать счет dbcontext
            string databaseName2 = "DbArve.db3";
            arveDatabase = new ArveDbContext(databaseName2);

            _ostukorv = new Ostukorv();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
