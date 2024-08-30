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
    public class ExoneracionPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<ExoneracionModel> _Exoneraciones;
        int _CurrentPage = 1;
        int _PageSize = 10;
        bool _IsRefreshing;
        bool _IsBusy;
        string _Cliente;
        #endregion

        #region CONSTRUCTOR
        public ExoneracionPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            string token = SecureStorage.GetAsync("token").Result;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            _Cliente = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            Exoneraciones = new ObservableCollection<ExoneracionModel>();
            RefreshData();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<ExoneracionModel> Exoneraciones
        {
            get { return _Exoneraciones ?? (_Exoneraciones = new ObservableCollection<ExoneracionModel>()); }
            set
            {
                SetValue(ref _Exoneraciones, value);
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
        public async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            _CurrentPage = 1;

            try
            {
                var exoneraciones = await ExoneracionData.ListExoneracion(_Cliente, _CurrentPage.ToString(), _PageSize.ToString());

                if (exoneraciones != null)
                {
                    Exoneraciones.Clear();
                    foreach (var exoneracion in exoneraciones)
                    {
                        Exoneraciones.Add(exoneracion);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron exoneraciones.", "OK");
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
                var exoneraciones = await ExoneracionData.ListExoneracion(_Cliente, _CurrentPage.ToString(), _PageSize.ToString());

                if (exoneraciones != null)
                {
                    foreach (var exoneracion in exoneraciones)
                    {
                        Exoneraciones.Add(exoneracion);
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

        public async Task Detalle(ExoneracionModel parametros)
        {
            await Navigation.PushAsync(new ExoneracionDetailPage(parametros));
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand DetalleCommand => new Command<ExoneracionModel>(async (p) => await Detalle(p));
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand LoadMoreCommand => new Command(async () => await LoadMoreData());
        public ICommand RefreshCommand => new Command(async () => await RefreshData());
        #endregion
    }
}