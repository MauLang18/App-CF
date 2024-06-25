using App_CF.Data;
using App_CF.Model;
using App_CF.View;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class TransInternacionalPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<TransInternacionalModel> _Casos;
        #endregion

        #region CONSTRUCTOR
        public TransInternacionalPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Mostrar();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<TransInternacionalModel> Casos
        {
            get { return _Casos; }
            set
            {
                SetValue(ref _Casos, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public async void Mostrar()
        {
            string token = SecureStorage.GetAsync("token").Result;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            string cliente = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            Casos = await TransInternacionalData.ListTransInternacional(cliente);
        }

        public async Task Detalle(TransInternacionalModel parametros)
        {
            await Navigation.PushAsync(new TransInternacionalDetailPage(parametros));
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand DetalleCommand => new Command<ExoneracionModel>(async (p) => await Detalle(p));
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        #endregion
    }
}
