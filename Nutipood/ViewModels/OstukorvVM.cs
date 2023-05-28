using Nutipood.Models;
using Nutipood.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Nutipood.ViewModels
{
    public class OstukorvVM : BaseViewModel
    {

        //об изменениях в общем количестве и сумме корзины будет сообщено на страницу
        private int _count;
        public int Count
        {
            get { return _count; }
            set { SetValue(ref _count, value); }
        }

        private double _total;
        public double Total
        {
            get { return _total; }
            set { SetValue(ref _total, value); }
        }


        private ObservableCollection<SmartDevice> _content;

        public ObservableCollection<SmartDevice> Content
        {
            get { return _content; }
            set { SetValue(ref _content, value); }
        }

        
        public Command Makse { get; }
        public Command EmptyOstukorv { get; }
        public Command Supprimer { get; }
        public OstukorvVM()
        {
            //список оглавления инициализируется из модели корзины
            this.Content = new ObservableCollection<SmartDevice>(App.Ostukorv.GetContent());

            this.Makse = new Command(OnMakse, Validate);
            this.EmptyOstukorv = new Command(ClearList);
            this.Supprimer = new Command<SmartDevice>(DeleteItem);

            // уведомляет модель представления об изменении Count и Total.

            this.PropertyChanged += (_, __) => Makse.ChangeCanExecute();


        }

        private async void OnMakse()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Hoiatus", "Jätka maksmist", "Jah", "Ei");

            if (answer)
            {
                await Shell.Current.GoToAsync(nameof(MaksePage));
            }
        }

        private bool Validate()
        {
            // проверить модель представления, если корзина все еще пуста, если нет, активировать кнопку оплаты.

            return this.Content.Count != 0;
        }

        private async void DeleteItem(SmartDevice item)
        {
            //подтверждающее сообщение перед удалением
            bool answer = await Application.Current.MainPage.DisplayAlert("Hoiatus", "Kas soovite seda toodet kustutada?", "Jah", "Ei");

            if (answer)
            {
                App.Ostukorv.RemoveProduct(item);
                await Application.Current.MainPage.DisplayAlert("Kinnitus", "Toode edukalt kustutatud", "OK");
            }

            //перезагружать страницу каждый раз, когда мы что-то удаляеем

            LoadContents();

        }

        private void ClearList()
        {
            App.Ostukorv.ClearOstukorv();
            LoadContents();

        }


        public void RefreshList()
        {
            LoadContents();
        }
        private void LoadContents()
        {
            try
            {
                this.Content.Clear();
                var items = App.Ostukorv.GetContent();
                foreach (var item in items)
                {
                    this.Content.Add(item);
                }
                this.Count = App.Ostukorv.CountOstukorv();
                this.Total = App.Ostukorv.GetTotal();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


        }
    }
}
