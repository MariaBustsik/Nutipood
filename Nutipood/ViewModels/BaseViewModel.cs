using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nutipood.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //Определение шаблона модели представления, от которого должны наследоваться модели представления, чтобы избежать
        //дублирования метода PropertyChanged и иметь более чистый код.

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //общий метод модели для всех установщиков свойств 
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            //если значение изменено, ввести новое значение и уведомить страницу, в противном случае возврат. 

            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;
            backingField = value;

            OnPropertyChanged(propertyName);
        }
    }
}
