using App_CF.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class ExoneracionDetailPageViewModel : BaseViewModel
    {
        #region VARIABLES
        public ExoneracionModel Model { get; set; }
        string _Solicitud;
        string _Autorizacion;
        string _Imagen;
        #endregion

        #region CONSTRUCTOR
        public ExoneracionDetailPageViewModel(INavigation navigation, ExoneracionModel exoneracion)
        {
            Navigation = navigation;
            Model = exoneracion;
            Solicitud = $"https://docs.google.com/viewer?url={exoneracion.Solicitud}";
            Autorizacion = $"https://docs.google.com/viewer?url={exoneracion.Autorizacion}";
            Imagen = "bg.jpg";
        }
        #endregion

        #region OBJETOS
        public string Solicitud
        {
            get => _Solicitud;
            set
            {
                _Solicitud = value;
                OnPropertyChanged(nameof(Solicitud));
            }
        }

        public string Autorizacion
        {
            get => _Autorizacion;
            set
            {
                _Autorizacion = value;
                OnPropertyChanged(nameof(Autorizacion));
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