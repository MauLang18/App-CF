using App_CF.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class WHSPageViewModel : BaseViewModel
    {
        #region VARIABLES

        #endregion

        #region CONSTRUCTOR
        public WHSPageViewModel(INavigation navigation)
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

        public async Task Guatemala()
        {
            await Navigation.PushAsync(new WHSListPage("Ciudad Guatemala, Guatemala"));
        }

        public async Task Honduras()
        {
            await Navigation.PushAsync(new WHSListPage("San Pedro Sula, Honduras"));
        }

        public async Task USA()
        {
            await Navigation.PushAsync(new WHSListPage("Miami, USA"));
        }

        public async Task Panama()
        {
            await Navigation.PushAsync(new WHSListPage("CFZ, Panama"));
        }

        public async Task CRC()
        {
            await Navigation.PushAsync(new WHSListPage("SJO, CRC"));
        }

        public async Task Ningbo()
        {
            await Navigation.PushAsync(new WHSListPage("Ningbo, China"));
        }

        public async Task Shanghai()
        {
            await Navigation.PushAsync(new WHSListPage("Shanghai, China"));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand HondurasCommand => new Command(async () => await Honduras());
        public ICommand GuatemalaCommand => new Command(async () => await Guatemala());
        public ICommand ShanghaiCommand => new Command(async () => await Shanghai());
        public ICommand NingboCommand => new Command(async () => await Ningbo());
        public ICommand SJOCommand => new Command(async () => await CRC());
        public ICommand CFZCommand => new Command(async () => await Panama());
        public ICommand MiamiCommand => new Command(async () => await USA());
        #endregion
    }
}