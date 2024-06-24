using App_CF.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class ItinerarioPageViewModel : BaseViewModel
    {
        #region Variables
        ObservableCollection<string> _PolOptions;
        ObservableCollection<string> _PodOptions;
        ObservableCollection<string> _TransporteOptions;
        ObservableCollection<string> _ModalidadOptions;
        string _SelectedPolOption;
        string _SelectedPodOption;
        string _SelectedTransporteOption;
        string _SelectedModalidadOption;
        #endregion

        #region Constructor
        public ItinerarioPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializeData();
        }
        #endregion

        #region Objetos
        public ObservableCollection<string> PolOptions
        {
            get => _PolOptions;
            set => SetProperty(ref _PolOptions, value);
        }

        public ObservableCollection<string> PodOptions
        {
            get => _PodOptions;
            set => SetProperty(ref _PodOptions, value);
        }

        public ObservableCollection<string> TransporteOptions
        {
            get => _TransporteOptions;
            set => SetProperty(ref _TransporteOptions, value);
        }

        public ObservableCollection<string> ModalidadOptions
        {
            get => _ModalidadOptions;
            set => SetProperty(ref _ModalidadOptions, value);
        }

        public string SelectedPolOption
        {
            get => _SelectedPolOption;
            set => SetProperty(ref _SelectedPolOption, value);
        }

        public string SelectedPodOption
        {
            get => _SelectedPodOption;
            set => SetProperty(ref _SelectedPodOption, value);
        }

        public string SelectedTransporteOption
        {
            get => _SelectedTransporteOption;
            set => SetProperty(ref _SelectedTransporteOption, value);
        }

        public string SelectedModalidadOption
        {
            get => _SelectedModalidadOption;
            set => SetProperty(ref _SelectedModalidadOption, value);
        }
        #endregion

        #region Procesos
        public void InitializeData()
        {
            PolOptions = new ObservableCollection<string>
            {
                "Ningbo, China",
                "Shanghai, China",
                "Qingdao, China",
                "Xiamen, China",
                "Yantian, China",
                "Guangzhou, China",
                "Miami, USA",
                "SJO, CRC",
                "PVG, China",
                "NKG, China",
                "PEK, China",
                "CFZ, Panama",
                "Ciudad Hidalgo, MX",
                "Ciudad de Guatemala, Guatemala",
                "Managua, Nicaragua",
                "San Pedro Sula, Honduras",
                "San Salvador, El Salvador",
                "Puerto Moin, CRC",
                "Puerto Caldera, CRC"
            };

            PodOptions = new ObservableCollection<string>
            {
                "CFZ, Panama",
                "SJO, CRC",
                "Ciudad Guatemala, Guatemala",
                "San Pedro Sula, Honduras",
                "San Salvador, El Salvador",
                "Managua, Nicaragua",
                "Newark (New Jersey), USA",
                "Los Angeles (California), USA",
                "Port Everglades (Florida), USA",
                "Savannah (Georgia), USA",
                "Wilmington (North Carolina), USA",
                "Houston (Texas), USA"
            };

            TransporteOptions = new ObservableCollection<string>
            {
                "Aereo",
                "Maritimo",
                "Terrestre"
            };

            ModalidadOptions = new ObservableCollection<string>
            {
                "LCL",
                "LTL",
                "FCL",
                "FTL",
                "Multimodal"
            };
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task Search()
        {
            await Navigation.PushAsync(new ItinerarioDetailPage(
                SelectedPolOption,
                SelectedPodOption,
                SelectedTransporteOption,
                SelectedModalidadOption));
        }
        #endregion

        #region Comandos
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand SearchCommand => new Command(async () => await Search());
        #endregion
    }
}