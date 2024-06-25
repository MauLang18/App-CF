﻿using App_CF.Model;
using App_CF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_CF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransInternacionalDetailPage : ContentPage
    {
        public TransInternacionalDetailPage(TransInternacionalModel model)
        {
            InitializeComponent();
            BindingContext = new TransInternacionalDetailPageViewModel(Navigation, model);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DetailsView.TranslationY = 600;
            DetailsView.TranslateTo(0, 0, 500, Easing.SinInOut);
        }
    }
}