using App_CF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_CF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancePage : ContentPage
    {
        public FinancePage()
        {
            InitializeComponent();
            BindingContext = new FinancePageViewModel(Navigation);
        }
    }
}