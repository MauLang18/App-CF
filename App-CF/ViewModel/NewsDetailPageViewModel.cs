using App_CF.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class NewsDetailPageViewModel : BaseViewModel
    {
        #region VARIABLES
        public NewsModel Model { get; set; }
        string _Contenido;
        string _Imagen;
        #endregion

        #region CONSTRUCTOR
        public NewsDetailPageViewModel(INavigation navigation, NewsModel news)
        {
            Navigation = navigation;
            Contenido = news.Contenido;
            Imagen = news.Imagen;
            Model = news;
        }
        #endregion

        #region OBJETOS
        public string Contenido
        {
            get => _Contenido;
            set
            {
                _Contenido = value;
                OnPropertyChanged(nameof(Contenido));
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