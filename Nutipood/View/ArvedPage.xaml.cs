﻿using Nutipood.ViewModels;
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
    public partial class ArvedPage : ContentPage
    {
        ArvedPageVM viewModel;
        public ArvedPage()
        {
            InitializeComponent();
            viewModel = new ArvedPageVM();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.RefreshList();
            BindingContext = null;
            BindingContext = this.viewModel;
        }


    }
}