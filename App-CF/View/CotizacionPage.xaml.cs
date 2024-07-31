using App_CF.ViewModel;
using Xamarin.Forms;

namespace App_CF.View
{
    public partial class CotizacionPage : ContentPage
    {
        public CotizacionPage()
        {
            InitializeComponent();
            BindingContext = new CotizacionPageViewModel(Navigation);
        }
    }
}