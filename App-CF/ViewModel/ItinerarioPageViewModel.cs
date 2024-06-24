using App_CF.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class ItinerarioPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<string> _PolOptions;
        ObservableCollection<string> _PodOptions;
        ObservableCollection<string> _TransporteOptions;
        ObservableCollection<string> _ModalidadOptions;
        string _SelectedPolOption;
        string _SelectedPodOption;
        string _SelectedTransporteOption;
        string _SelectedModalidadOption;
        int _CurrentStep;
        #endregion

        #region CONSTRUCTOR
        public ItinerarioPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializeData();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<string> PolOptions
        {
            get => _PolOptions;
            set
            {
                SetValue(ref _PolOptions, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> PodOptions
        {
            get => _PodOptions;
            set
            {
                SetValue(ref _PodOptions, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> TransporteOptions
        {
            get => _TransporteOptions;
            set
            {
                SetValue(ref _TransporteOptions, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ModalidadOptions
        {
            get => _ModalidadOptions;
            set
            {
                SetValue(ref _ModalidadOptions, value);
                OnPropertyChanged();
            }
        }

        public string SelectedPolOption
        {
            get => _SelectedPolOption;
            set
            {
                SetValue(ref _SelectedPolOption, value);
                OnPropertyChanged();
            }
        }

        public string SelectedPodOption
        {
            get => _SelectedPodOption;
            set
            {
                SetValue(ref _SelectedPodOption, value);
                OnPropertyChanged();
            }
        }

        public string SelectedTransporteOption
        {
            get => _SelectedTransporteOption;
            set
            {
                SetValue(ref _SelectedTransporteOption, value);
                OnPropertyChanged();
            }
        }

        public string SelectedModalidadOption
        {
            get => _SelectedModalidadOption;
            set
            {
                SetValue(ref _SelectedModalidadOption, value);
                OnPropertyChanged();
            }
        }

        public int CurrentStep
        {
            get => _CurrentStep;
            set
            {
                SetValue(ref _CurrentStep, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public string ButtonText => CurrentStep < 3 ? "Siguiente" : "Buscar";
        #endregion

        #region PROCESOS
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

            CurrentStep = 0;
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task NextStep()
        {
            if (CurrentStep < 3)
            {
                CurrentStep++;
            }
            else
            {
                await Navigation.PushAsync(new ItinerarioDetailPage(SelectedPolOption, SelectedPodOption, SelectedTransporteOption, SelectedModalidadOption));
            }
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand NextCommand => new Command(async () => await NextStep());
        #endregion
    }
}