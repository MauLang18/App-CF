using App_CF.Data;
using App_CF.Model;
using System;
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
        int _CurrentPage = 1;
        int _PageSize = 10;
        bool _IsRefreshing;
        bool _IsBusy;
        string _Pol;
        string _Pod;
        string _Transporte;
        string _Modalidad;
        #endregion

        #region CONSTRUCTOR
        public ItinerarioDetailPageViewModel(INavigation navigation, string pol, string pod, string transporte, string modalidad)
        {
            Navigation = navigation;
            _Pol = pol;
            _Pod = pod;
            _Transporte = transporte;
            _Modalidad = modalidad;
            Itinerarios = new ObservableCollection<ItinerarioModel>();
            RefreshData(); // Cargar los datos iniciales
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<ItinerarioModel> Itinerarios
        {
            get { return _Itinerarios ?? (_Itinerarios = new ObservableCollection<ItinerarioModel>()); }
            set { SetValue(ref _Itinerarios, value); }
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
            _CurrentPage = 1; // Reiniciar la página actual para la carga inicial

            try
            {
                var itinerarios = await ItinerarioData.ListItinerario(_Pol, _Pod, _Transporte, _Modalidad, _CurrentPage.ToString(), _PageSize.ToString());

                if (itinerarios != null)
                {
                    Itinerarios.Clear();
                    foreach (var itinerario in itinerarios)
                    {
                        Itinerarios.Add(itinerario);
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
                var itinerarios = await ItinerarioData.ListItinerario(_Pol, _Pod, _Transporte, _Modalidad, _CurrentPage.ToString(), _PageSize.ToString());

                if (itinerarios != null)
                {
                    foreach (var itinerario in itinerarios)
                    {
                        Itinerarios.Add(itinerario);
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

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());

        public ICommand LoadMoreCommand => new Command(async () => await LoadMoreData());

        public ICommand RefreshCommand => new Command(async () => await RefreshData());

        #endregion
    }
}