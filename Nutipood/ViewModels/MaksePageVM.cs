using Nutipood.Models;
using Nutipood.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nutipood.ViewModels
{
    public class MaksePageVM : BaseViewModel
    {

        //Имя и фамилия клиента являются обязательными и будут отображаться в счете,
        //но это выборочно, можно сделать это для всех полей

        private string _addNimi;

        //поскольку кнопка оплаты является условной, каждый раз при изменении
        //значения в текстовом поле имени и фамилии должно быть отправлено уведомление в обоих направлениях.
        public string AddNimi
        {
            get { return _addNimi; }
            set
            {
                SetValue(ref _addNimi, value);
            }
        }

        private string _addPerekonnaNimi;
        public string AddPerekonnaNimi
        {
            get { return _addPerekonnaNimi; }
            set { SetValue(ref _addPerekonnaNimi, value); }
        }
        public string AddAadress { get; set; }
        public string AddTel { get; set; }
        public string AddEpost { get; set; }
        public string AddPankNumber { get; set; }
        public double Summa { get; set; }




        
        public Command CmdAdd { get; private set; }

        public Command CmdCancel { get; private set; }


        public MaksePageVM()
        {
            CmdAdd = new Command(async () => await AddArveAsync(), ValidateSave);

            CmdCancel = new Command(OnCancel);

            //  каждый раз, когда одно из текстовых полей меняет значение (выполнить метод validateSave)
            this.PropertyChanged += (_, __) => CmdAdd.ChangeCanExecute();

            //сумма перечисляется из корзины
            this.Summa = App.Ostukorv.GetTotal();

        }

        private bool ValidateSave()
        {
            //кнопка оплаты будет активирована только в том случае, если введены имя и фамилия

            return !string.IsNullOrWhiteSpace(AddNimi) && !string.IsNullOrWhiteSpace(AddPerekonnaNimi);
        }



        //определение действия добавить
        public async Task AddArveAsync()
        {
            //Создание объекта Klient со значениями в текстовом поле

            Klient klient = new Klient()
            {
                Nimi = AddNimi,
                PerekonnaNimi = AddPerekonnaNimi,
                Aadress = AddAadress,
                Telefon = AddTel,
                Epost = AddEpost,
                PankNumber = AddPankNumber
            };

            //Создание объекта Arve с суммой и именем клиента
            Arve newArve = new Arve()
            {

                Summa = this.Summa,
                KlientNimi = $"{klient.Nimi} {klient.PerekonnaNimi}",

            };

            //сообщение пользователю для подтверждения оплаты
            bool answer = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Hoiatus", "Kinnita makse?", "Jah", "Ei");

            if (answer)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Kinnitus", "Makse töödeldud, mine arve lehele", "OK");

                //когда платеж подтвержден, добавить новый счет в базу данных.

                await App.ArveDatabase.InsertAsync(newArve);

                //перенаправление на страницу счетов
                await Shell.Current.GoToAsync(nameof(ArvedPage));
            }
        }

        //Нажатие на кнопку отмены перенаправляет нас на домашнюю страницу.
        private async void OnCancel(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));

        }

    }
}
