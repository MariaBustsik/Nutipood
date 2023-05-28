using Nutipood.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Nutipood.ViewModels
{
    public class ArvedPageVM : BaseViewModel
    {
        private ObservableCollection<Arve> _listArved;

        //об изменении списка счетов необходимо уведомить
        public ObservableCollection<Arve> ListArved
        {
            get { return _listArved; }
            set { SetValue(ref _listArved, value); }
        }

        //определить команду удаления для вызова метода удаления
        public Command Supprimer { get; }

        public ArvedPageVM()
        {
            this.ListArved = new ObservableCollection<Arve>();
            this.Supprimer = new Command<Arve>(DeleteItem);

        }

        private async void DeleteItem(Arve f)
        {
            //диалоговое окно, в котором пользователю предлагается подтвердить действие удаления

            bool answer = await Application.Current.MainPage.DisplayAlert("Hoiatus", "Kas soovite selle arve kustutada?", "Jah", "Ei");

            if (answer)
            {
                //вызов метода удаления из базы данных
                await App.ArveDatabase.DeleteAsync(f);
                await Application.Current.MainPage.DisplayAlert("Kinnitus", "Arve edukalt kustutatud", "OK");
            }
            //перезагрузить список
            LoadSmartDevices();
        }

        public void RefreshList()
        {
            LoadSmartDevices();
        }
        private async void LoadSmartDevices()
        {
            try
            {
                this.ListArved.Clear();
                var items = await App.ArveDatabase.GetAllAsync();
                foreach (var item in items)
                {
                    this.ListArved.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
