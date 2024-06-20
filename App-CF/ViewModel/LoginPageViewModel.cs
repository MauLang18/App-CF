using App_CF.Data;
using App_CF.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Correo;
        string _Pass;
        #endregion

        #region CONSTRUCTOR
        public LoginPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS
        public string Correo
        {
            get { return _Correo; }
            set
            {
                SetValue(ref _Correo, value);
                OnPropertyChanged();
            }
        }

        public string Pass
        {
            get { return _Pass; }
            set
            {
                SetValue(ref _Pass, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public async Task Login()
        {
            ApiResponseToken response = await LoginData.Login(Correo, Pass);
            Console.WriteLine(Correo, Pass);
            if (response is null)
            {
                await DisplayAlert("Usuario y/o contraseña incorrectas", "", "OK");
            }
            else
            {
                await SecureStorage.SetAsync("token", response.Data.ToString());
                await Navigation.PushAsync(new MainPage());
            }
        }
        #endregion

        #region COMANDOS
        public ICommand LoginCommand => new Command(async () => await Login());
        #endregion
    }
}