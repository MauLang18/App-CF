using App_CF.Data;
using App_CF.Model;
using App_CF.View;
using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class WHSListPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<WHSModel> _WHS;
        int _CurrentPage = 1;
        int _PageSize = 10;
        bool _IsRefreshing;
        bool _IsBusy;
        string _Cliente;
        string _Whs;
        #endregion

        #region CONSTRUCTOR
        public WHSListPageViewModel(INavigation navigation, string whs)
        {
            Navigation = navigation;
            string token = SecureStorage.GetAsync("token").Result;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            _Cliente = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            _Whs = whs;
            WHS = new ObservableCollection<WHSModel>();
            RefreshData();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<WHSModel> WHS
        {
            get { return _WHS ?? (_WHS = new ObservableCollection<WHSModel>()); }
            set
            {
                SetValue(ref _WHS, value);
            }
        }

        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { SetValue(ref _IsRefreshing, value); }
        }

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetValue(ref _IsBusy, value); }
        }
        #endregion

        #region PROCESOS
        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            _CurrentPage = 1;

            try
            {
                var whs = await WHSData.ListWHS(_Cliente, _Whs, _CurrentPage.ToString(), _PageSize.ToString());

                if (whs != null)
                {
                    WHS.Clear();
                    foreach (var data in whs)
                    {
                        WHS.Add(data);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron whs.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        public async Task LoadMoreData()
        {
            if (IsBusy)
                return;

            _CurrentPage++;
            IsBusy = true;

            try
            {
                var whs = await WHSData.ListWHS(_Cliente, _Whs, _CurrentPage.ToString(), _PageSize.ToString());

                if (whs != null)
                {
                    foreach (var data in whs)
                    {
                        WHS.Add(data);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron itinerarios.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task Detalle(WHSModel parametros)
        {
            await Navigation.PushAsync(new WHSDetailPage(parametros));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand DetalleCommand => new Command<WHSModel>(async (p) => await Detalle(p));
        public ICommand LoadMoreCommand => new Command(async () => await LoadMoreData());
        public ICommand RefreshCommand => new Command(async () => await RefreshData());
        #endregion
    }
}
