using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Nombre;
        string _Empresa;
        string _Correo;
        string _Imagen;
        #endregion

        #region CONSTRUCTOR
        public ProfilePageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            string token = SecureStorage.GetAsync("token").Result;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken.Claims.FirstOrDefault(c => c.Type == "gender")?.Value is null)
            {
                Imagen = "bg.jpg";
            }
            else
            {
                Imagen = jwtToken.Claims.FirstOrDefault(c => c.Type == "gender")?.Value;
            }

            Correo = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            Empresa = jwtToken.Claims.FirstOrDefault(c => c.Type == "acr")?.Value;
            Nombre = jwtToken.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
        }
        #endregion

        #region OBJETOS
        public string Nombre
        {
            get => _Nombre;
            set
            {
                _Nombre = value;
                OnPropertyChanged(nameof(_Nombre));
            }
        }

        public string Empresa
        {
            get => _Empresa;
            set
            {
                _Empresa = value;
                OnPropertyChanged(nameof(_Empresa));
            }
        }

        public string Correo
        {
            get => _Correo;
            set
            {
                _Correo = value;
                OnPropertyChanged(nameof(_Correo));
            }
        }

        public string Imagen
        {
            get => _Imagen;
            set
            {
                _Imagen = value;
                OnPropertyChanged(nameof(Imagen));
            }
        }
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