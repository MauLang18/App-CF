using App_CF.Data;
using App_CF.Model;
using App_CF.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<NewsModel> _News;
        ObservableCollection<NewsModel> _LatestNews;
        List<MenuItems> _Menu;
        bool _IsRefreshing;
        #endregion

        #region CONSTRUCTOR
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Mostrar();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<NewsModel> LatestNews
        {
            get { return _LatestNews; }
            set
            {
                SetValue(ref _LatestNews, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NewsModel> News
        {
            get { return _News; }
            set
            {
                SetValue(ref _News, value);
                OnPropertyChanged();
            }
        }

        public List<MenuItems> Menu
        {
            get { return _Menu; }
            set
            {
                SetValue(ref _Menu, value);
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetProperty(ref _IsRefreshing, value);
        }
        #endregion

        #region PROCESOS
        public async void Mostrar()
        {
            News = await NewsData.ListNews();
            LatestNews = await NewsData.ListLatestNews();
            Menu = GetMenus();
        }

        public async Task Detalle(NewsModel parametros)
        {
            await Navigation.PushAsync(new NewsDetailPage(parametros));
        }

        private List<MenuItems> GetMenus()
        {
            string token = SecureStorage.GetAsync("token").Result;

            if (string.IsNullOrEmpty(token))
            {
                return new List<MenuItems>
                {
                    new MenuItems { Name = "Home", Icon = "home.png" }
                };
            }
            else
            {
                var menus = new List<MenuItems>
                {
                    new MenuItems { Name = "Home", Icon = "home.png" },
                    new MenuItems { Name = "Perfil", Icon = "profile.png", Command = ProfileCommand },
                    new MenuItems { Name = "Transporte Internacional", Icon = "transportation.png", Command = TransporteInternacionalCommand },
                    new MenuItems { Name = "Agenciamiento Aduanal", Icon = "paper.png", Command = AgenciamientoAduanalCommand },
                    new MenuItems { Name = "Itinerarios", Icon = "schedule.png", Command = ItinerariosCommand },
                    new MenuItems { Name = "Tracking", Icon = "tracking.png", Command = TrackingCommand },
                    new MenuItems { Name = "Cotización", Icon = "quote.png", Command = CotizacionCommand },
                    new MenuItems { Name = "My Documentacion", Icon = "documents.png", Command = MyDocumentationCommand },
                    new MenuItems { Name = "Exoneraciones", Icon = "documents.png", Command = ExoneracionesCommand },
                    new MenuItems { Name = "WHS", Icon = "warehouse.png", Command = WHSCommand },
                    new MenuItems { Name = "My Finance", Icon = "finance.png", Command = MyFinanceCommand },
                    new MenuItems { Name = "Directorio Interno", Icon = "agenda.png", Command = DirectorioInternoCommand },

                };
                return menus;
            }
        }

        public async Task RefreshData()
        {
            IsRefreshing = true;
            await Task.Delay(2000);
            News = await NewsData.ListNews();
            LatestNews = await NewsData.ListLatestNews();
            IsRefreshing = false;
        }

        public async Task Login()
        {
            await Navigation.PushAsync(new LoginPage());
        }

        //Home
        public async Task Home()
        {
            await Navigation.PushAsync(new MainPage());
        }
        //Profile
        public async Task Profile()
        {
            await Navigation.PushAsync(new ProfilePage());
        }
        //TransporteInternacional
        public async Task TransInternacional()
        {
            await Navigation.PushAsync(new TransInternacionalPage());
        }
        //AgenciamientoAduanal
        public async Task AgenAduanal()
        {
            await Navigation.PushAsync(new AgenAduanalPage());
        }
        //Itinerario
        public async Task Itinerario()
        {
            await Navigation.PushAsync(new ItinerarioPage());
        }
        //Tracking
        public async Task Tracking()
        {
            await Navigation.PushAsync(new TrackingPage());
        }
        //Cotizacion
        public async Task Cotizacion()
        {
            await Navigation.PushAsync(new CotizacionPage());
        }
        //MyDocumentacion
        public async Task Documentacion()
        {
            await Navigation.PushAsync(new DocumentacionPage());
        }
        //Exoneraciones
        public async Task Exoneracion()
        {
            await Navigation.PushAsync(new ExoneracionPage());
        }
        //WHS
        public async Task WHS()
        {
            await Navigation.PushAsync(new WHSPage());
        }
        //MyFinance
        public async Task Finance()
        {
            await Navigation.PushAsync(new FinancePage());
        }
        //DirectorioInterno
        public async Task DirecInterno()
        {
            await Navigation.PushAsync(new DirecInternoPage());
        }

        #endregion

        #region COMANDOS
        public ICommand DetalleCommand => new Command<NewsModel>(async (p) => await Detalle(p));
        public ICommand RefreshCommand => new Command(async () => await RefreshData());

        public ICommand LoginCommand => new Command(async () => await Login());
        public ICommand ProfileCommand => new Command(async () => await Profile());
        public ICommand TransporteInternacionalCommand => new Command(async () => await TransInternacional());
        public ICommand AgenciamientoAduanalCommand => new Command(async () => await AgenAduanal());
        public ICommand ItinerariosCommand => new Command(async () => await Itinerario());
        public ICommand TrackingCommand => new Command(async () => await Tracking());
        public ICommand CotizacionCommand => new Command(async () => await Cotizacion());
        public ICommand MyDocumentationCommand => new Command(async () => await Documentacion());
        public ICommand ExoneracionesCommand => new Command(async () => await Exoneracion());
        public ICommand WHSCommand => new Command(async () => await WHS());
        public ICommand MyFinanceCommand => new Command(async () => await Finance());
        public ICommand DirectorioInternoCommand => new Command(async () => await DirecInterno());
        #endregion
    }
}