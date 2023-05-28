using Nutipood.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Nutipood.ViewModels
{
    public class ItemsPageVM : BaseViewModel
    {
        public ObservableCollection<SmartDevice> SmartDevices
        {
            get;
            set;
        }

        //общее количество в корзине
        private int _count;

        //об изменении Kokku должно быть сообщено на страницу
        public int Count
        {
            get { return _count; }
            set { SetValue(ref _count, value); }
        }

        //Команда для действия двойного щелчка
        public Command<SmartDevice> ItemTapped { get; }

        public Command EmptyOstukorv { get; }

        public ItemsPageVM()
        {
            //Двойным щелчком мыши Сохранить в корзину.
            this.ItemTapped = new Command<SmartDevice>(OnAddSmartDevice);
            this.EmptyOstukorv = new Command(ClearList);
            this.SmartDevices = new ObservableCollection<SmartDevice>();
        }

        public void RefreshList()
        {
            LoadSmartDevices();
        }
        private async void LoadSmartDevices()
        {
            try
            {
                this.SmartDevices.Clear();
                var items = await App.DeviceDatabase.GetAllAsync();
                foreach (var item in items)
                {
                    this.SmartDevices.Add(item);
                }

                //Определите Count как общее количество в корзине.

                this.Count = App.Ostukorv.CountOstukorv();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void OnAddSmartDevice(SmartDevice item)
        {

            App.Ostukorv.AddProduct(item);
            this.Count = App.Ostukorv.CountOstukorv();

        }
        private void ClearList()
        {
            App.Ostukorv.ClearOstukorv();
            this.Count = App.Ostukorv.CountOstukorv();
        }


    }
}
