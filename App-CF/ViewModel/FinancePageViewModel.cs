using App_CF.Data;
using App_CF.Model;
using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class FinancePageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<FinanceModel> _Finance;
        string _EstadoCuenta;
        string _Cliente;
        string _EstadoCuentaURL;
        #endregion

        #region CONSTRUCTOR
        public FinancePageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Mostrar();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<FinanceModel> Finance
        {
            get { return _Finance; }
            set
            {
                SetValue(ref _Finance, value);
                OnPropertyChanged();
            }
        }

        public string EstadoCuenta
        {
            get => _EstadoCuenta;
            set
            {
                _EstadoCuenta = value;
                OnPropertyChanged(nameof(EstadoCuenta));
            }
        }

        public string Cliente
        {
            get => _Cliente;
            set
            {
                _Cliente = value;
                OnPropertyChanged(nameof(Cliente));
            }
        }

        public string EstadoCuentaURL
        {
            get => _EstadoCuentaURL;
            set
            {
                _EstadoCuentaURL = value;
                OnPropertyChanged(nameof(EstadoCuentaURL));
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
            Finance = await FinanceData.ListFinance(cliente);
            foreach (var ex in Finance)
            {
                EstadoCuentaURL = ex.EstadoCuenta;
                Cliente = ex.NombreCliente;
                EstadoCuenta = $"https://docs.google.com/viewer?url={ex.EstadoCuenta}";
            }
        }
        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        public void OpenUrl()
        {
            Launcher.OpenAsync(new Uri(EstadoCuentaURL));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand EnlaceCommand => new Command(OpenUrl);
        #endregion
    }
}