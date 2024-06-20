using App_CF.ViewModel;
using System;
using Xamarin.Forms;

namespace App_CF
{
    public partial class MainPage : ContentPage
    {
        private bool isSwipeViewOpen = false;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }

        private async void OpenAnimation()
        {
            await swipeContent.ScaleYTo(0.9, 300, Easing.SinOut);
            pancake.CornerRadius = 20;
            await swipeContent.RotateTo(-15, 300, Easing.SinOut);
        }

        private async void CloseAnimation()
        {
            await swipeContent.RotateTo(0, 300, Easing.SinOut);
            pancake.CornerRadius = 0;
            await swipeContent.ScaleYTo(1, 300, Easing.SinOut);
        }

        private void OpenSwipe(object sender, EventArgs e)
        {
            if (isSwipeViewOpen)
            {
                CloseSwipe();
            }
            else
            {
                MainSwipeView.Open(OpenSwipeItem.LeftItems);
                OpenAnimation();
                isSwipeViewOpen = true;
            }
        }

        private void Prueba(object sender, EventArgs e)
        {
            DisplayAlert("Estoy en el box", "", "Ok.");
        }

        private void CloseSwipe()
        {
            MainSwipeView.Close();
            CloseAnimation();
            isSwipeViewOpen = false;
        }

        private void SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            OpenAnimation();
        }

        private void SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if (!e.IsOpen)
            {
                CloseSwipe();
            }
            else
            {
                isSwipeViewOpen = true;
            }
        }
    }
}