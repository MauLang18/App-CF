using App_CF.Data;
using App_CF.Model;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class TrackingPageViewModel : BaseViewModel
    {
        #region Variables
        ObservableCollection<string> _searchOptions;
        string _selectedSearchOption;
        string _searchText;
        ObservableCollection<TrackingModel> _searchResults;
        #endregion

        #region Constructor
        public TrackingPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializeData();
        }
        #endregion

        #region Properties
        public ObservableCollection<string> SearchOptions
        {
            get => _searchOptions;
            set => SetProperty(ref _searchOptions, value);
        }

        public string SelectedSearchOption
        {
            get => _selectedSearchOption;
            set => SetProperty(ref _selectedSearchOption, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ObservableCollection<TrackingModel> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }
        #endregion

        #region Methods
        public async Task InitializeData()
        {
            SearchOptions = new ObservableCollection<string> { "IDTRA", "#CONTENEDOR", "BCF", "PO" };
            SearchResults = new ObservableCollection<TrackingModel>();
        }

        public async Task Search()
        {
            string token = SecureStorage.GetAsync("token").Result;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            string cliente = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            SearchResults = await TrackingData.ListTracking(cliente, SearchText, SelectedSearchOption);
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand SearchCommand => new Command(async () => await Search());
        #endregion
    }
}