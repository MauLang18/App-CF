using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class AgenAduanalPageViewModel : BaseViewModel
    {
        #region VARIABLES

        #endregion

        #region CONSTRUCTOR
        public AgenAduanalPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS

        #endregion

        #region PROCESOS
        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        #endregion
    }
}