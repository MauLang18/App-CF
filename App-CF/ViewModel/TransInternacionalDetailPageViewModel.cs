using App_CF.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class TransInternacionalDetailPageViewModel : BaseViewModel
    {
        #region VARIABLES
        public TransInternacionalModel Model { get; set; }
        string _Imagen;
        #endregion

        #region CONSTRUCTOR
        public TransInternacionalDetailPageViewModel(INavigation navigation, TransInternacionalModel model)
        {
            Navigation = navigation;
            Model = model;
            Imagen = "bg.jpg";
        }
        #endregion

        #region OBJETOS
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
