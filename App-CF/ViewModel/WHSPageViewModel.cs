using App_CF.Data;
using App_CF.Model;
using App_CF.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class WHSPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<PuertoModel> _whsList;
        #endregion

        #region CONSTRUCTOR
        public WHSPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Mostrar();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<PuertoModel> WhsList
        {
            get { return _whsList; }
            set
            {
                _whsList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS

        public async void Mostrar()
        {
            var whsData = await OrigenData.GetFilteredWhs();
            var paisData = await OrigenData.GetPaises();

            foreach (var whs in whsData)
            {
                var paisName = whs.Nombre.Split(',').Last().Trim();
                var pais = paisData.FirstOrDefault(p => p.Description.Equals(paisName, StringComparison.OrdinalIgnoreCase));
                if (pais != null)
                {
                    whs.ImageUrl = pais.Id;
                }
            }

            WhsList = whsData;
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task NavigateToWHSListPage(string location)
        {
            await Navigation.PushAsync(new WHSListPage(location));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand NavigateToWHSListPageCommand => new Command<string>(async (location) => await NavigateToWHSListPage(location));
        #endregion
    }
}
