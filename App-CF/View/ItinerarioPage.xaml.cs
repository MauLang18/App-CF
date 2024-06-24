using App_CF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_CF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItinerarioPage : ContentPage
    {
        public ItinerarioPage()
        {
            InitializeComponent();
            BindingContext = new ItinerarioPageViewModel(Navigation);
        }
    }
}