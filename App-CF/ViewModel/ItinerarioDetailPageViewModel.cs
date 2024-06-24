using App_CF.Data;
using App_CF.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class ItinerarioDetailPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<ItinerarioModel> _Itinerarios;
        #endregion

        #region CONSTRUCTOR
        public ItinerarioDetailPageViewModel(INavigation navigation, string pol, string pod, string transporte, string modalidad)
        {
            Navigation = navigation;
            Mostrar(pol, pod, transporte, modalidad);
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<ItinerarioModel> Itinerarios
        {
            get => _Itinerarios;
            set
            {
                SetValue(ref _Itinerarios, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public async void Mostrar(string pol, string pod, string transporte, string modalidad)
        {
            Itinerarios = await ItinerarioData.ListItinerario(pol, pod, transporte, modalidad);
        }

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