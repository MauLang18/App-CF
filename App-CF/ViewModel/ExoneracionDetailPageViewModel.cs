using App_CF.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
        string _SolicitudURL;
        string _AutorizacionURL;
        #endregion

        #region CONSTRUCTOR
        public ExoneracionDetailPageViewModel(INavigation navigation, ExoneracionModel exoneracion)
        {
            Navigation = navigation;
            Model = exoneracion;
            Solicitud = $"https://docs.google.com/viewer?url={exoneracion.Solicitud}";
            Autorizacion = $"https://docs.google.com/viewer?url={exoneracion.Autorizacion}";
            SolicitudURL = exoneracion.Solicitud;
            AutorizacionURL = exoneracion.Autorizacion;
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

        public string SolicitudURL
        {
            get => _SolicitudURL;
            set
            {
                _SolicitudURL = value;
                OnPropertyChanged(nameof(SolicitudURL));
            }
        }

        public string AutorizacionURL
        {
            get => _AutorizacionURL;
            set
            {
                _AutorizacionURL = value;
                OnPropertyChanged(nameof(AutorizacionURL));
            }
        }
        #endregion

        #region PROCESOS
        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public void OpenUrlSolicitud()
        {
            Launcher.OpenAsync(new Uri(SolicitudURL));
        }

        public void OpenUrlAutorizacion()
        {
            Launcher.OpenAsync(new Uri(AutorizacionURL));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand SolicitudCommand => new Command(OpenUrlSolicitud);
        public ICommand AutorizacionCommand => new Command(OpenUrlAutorizacion);
        #endregion
    }
}