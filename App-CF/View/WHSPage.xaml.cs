﻿using App_CF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_CF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WHSPage : ContentPage
    {
        public WHSPage()
        {
            InitializeComponent();
            BindingContext = new WHSPageViewModel(Navigation);
        }
    }
}