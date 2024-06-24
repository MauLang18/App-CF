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
    public class WHSListPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<WHSModel> _WHS;
        #endregion

        #region CONSTRUCTOR
        public WHSListPageViewModel(INavigation navigation, string whs)
        {
            Navigation = navigation;
            Mostrar(whs);
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<WHSModel> WHS
        {
            get { return _WHS; }
            set
            {
                SetValue(ref _WHS, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async void Mostrar(string whs)
        {
            string token = SecureStorage.GetAsync("token").Result;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            string cliente = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            WHS = await WHSData.ListWHS(cliente, whs);
        }

        public async Task Detalle(WHSModel parametros)
        {
            await Navigation.PushAsync(new WHSDetailPage(parametros));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand DetalleCommand => new Command<WHSModel>(async (p) => await Detalle(p));
        #endregion
    }
}
