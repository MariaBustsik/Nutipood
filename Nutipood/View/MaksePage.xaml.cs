using Nutipood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nutipood.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaksePage : ContentPage
    {
        MaksePageVM viewModel;
        public MaksePage()
        {
            InitializeComponent();
            viewModel = new MaksePageVM();
            BindingContext = this.viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = this.viewModel;
        }


    }
}