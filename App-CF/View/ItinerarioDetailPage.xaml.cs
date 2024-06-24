using App_CF.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_CF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItinerarioDetailPage : ContentPage
    {
        public ItinerarioDetailPage(string pol, string pod, string transporte, string modalidad)
        {
            InitializeComponent();
            BindingContext = new ItinerarioDetailPageViewModel(Navigation, pol, pod, transporte, modalidad);
        }

        private async Task OpenAnimation(Xamarin.Forms.View view, uint length = 250)
        {
            view.RotationX = -90;
            view.IsVisible = true;
            view.Opacity = 0;
            _ = view.FadeTo(1, length);
            await view.RotateXTo(0, length);
        }

        private async Task CloseAnimation(Xamarin.Forms.View view, uint length = 250)
        {
            _ = view.FadeTo(0, length);
            await view.RotateXTo(-90, length);
            view.IsVisible = false;
        }

        private async void MainExpander_Tapped(object sender, EventArgs e)
        {
            var expander = sender as Expander;
            //var imgView = expander.FindByName<Grid>("ImageView");
            var detailsView = expander.FindByName<Grid>("DetailsView");

            if (expander.IsExpanded)
            {
                //await OpenAnimation(imgView);
                await OpenAnimation(detailsView);
            }
            else
            {
                await CloseAnimation(detailsView);
                //await CloseAnimation(imgView);
            }
        }
    }
}