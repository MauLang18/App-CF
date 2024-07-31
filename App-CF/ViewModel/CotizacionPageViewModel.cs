using App_CF.Data;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class CotizacionPageViewModel : BaseViewModel
    {
        #region Variables
        ObservableCollection<string> _IncotermOptions;
        ObservableCollection<string> _ServicioOptions;
        ObservableCollection<string> _OrigenOptions;
        ObservableCollection<string> _DestinoOptions;
        ObservableCollection<string> _ProductoOptions;
        string _SelectedIncotermOption;
        string _SelectedServicioOption;
        string _SelectedOrigenOption;
        string _SelectedDestinoOption;
        string _SelectedProductoOption;
        string _NombreEmpresa;
        string _Telefono;
        string _Correo;
        string _ValorMercancia;
        string _Peso;
        string _Volumen;
        string _Dimensiones;
        string _Observaciones;
        bool _SeguroCarga;
        bool _TransporteNacional;
        bool _AgenciaAduanas;
        bool _AlmacenFiscal;
        #endregion

        #region Constructor
        public CotizacionPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializeData();
        }
        #endregion

        #region Objetos
        public ObservableCollection<string> IncotermOptions
        {
            get => _IncotermOptions;
            set => SetProperty(ref _IncotermOptions, value);
        }

        public ObservableCollection<string> ServicioOptions
        {
            get => _ServicioOptions;
            set => SetProperty(ref _ServicioOptions, value);
        }

        public ObservableCollection<string> OrigenOptions
        {
            get => _OrigenOptions;
            set => SetProperty(ref _OrigenOptions, value);
        }

        public ObservableCollection<string> DestinoOptions
        {
            get => _DestinoOptions;
            set => SetProperty(ref _DestinoOptions, value);
        }

        public ObservableCollection<string> ProductoOptions
        {
            get => _ProductoOptions;
            set => SetProperty(ref _ProductoOptions, value);
        }

        public string SelectedIncotermOption
        {
            get => _SelectedIncotermOption;
            set => SetProperty(ref _SelectedIncotermOption, value);
        }

        public string SelectedServicioOption
        {
            get => _SelectedServicioOption;
            set => SetProperty(ref _SelectedServicioOption, value);
        }

        public string SelectedOrigenOption
        {
            get => _SelectedOrigenOption;
            set => SetProperty(ref _SelectedOrigenOption, value);
        }

        public string SelectedDestinoOption
        {
            get => _SelectedDestinoOption;
            set => SetProperty(ref _SelectedDestinoOption, value);
        }

        public string SelectedProductoOption
        {
            get => _SelectedProductoOption;
            set => SetProperty(ref _SelectedProductoOption, value);
        }

        public string NombreEmpresa
        {
            get => _NombreEmpresa;
            set => SetProperty(ref _NombreEmpresa, value);
        }

        public string Telefono
        {
            get => _Telefono;
            set => SetProperty(ref _Telefono, value);
        }

        public string Correo
        {
            get => _Correo;
            set => SetProperty(ref _Correo, value);
        }

        public string ValorMercancia
        {
            get => _ValorMercancia;
            set => SetProperty(ref _ValorMercancia, value);
        }

        public string Peso
        {
            get => _Peso;
            set => SetProperty(ref _Peso, value);
        }

        public string Volumen
        {
            get => _Volumen;
            set => SetProperty(ref _Volumen, value);
        }

        public string Dimensiones
        {
            get => _Dimensiones;
            set => SetProperty(ref _Dimensiones, value);
        }

        public string Observaciones
        {
            get => _Observaciones;
            set => SetProperty(ref _Observaciones, value);
        }

        public bool SeguroCarga
        {
            get => _SeguroCarga;
            set => SetProperty(ref _SeguroCarga, value);
        }

        public bool TransporteNacional
        {
            get => _TransporteNacional;
            set => SetProperty(ref _TransporteNacional, value);
        }

        public bool AgenciaAduanas
        {
            get => _AgenciaAduanas;
            set => SetProperty(ref _AgenciaAduanas, value);
        }

        public bool AlmacenFiscal
        {
            get => _AlmacenFiscal;
            set => SetProperty(ref _AlmacenFiscal, value);
        }
        #endregion

        #region Procesos
        public async Task InitializeData()
        {
            try
            {
                var puertoData = await OrigenData.GetPuertos();

                if (puertoData != null && puertoData.Count > 0)
                {
                    OrigenOptions = new ObservableCollection<string>(puertoData.Select(p => p.Description));
                    DestinoOptions = new ObservableCollection<string>(puertoData.Select(p => p.Description));
                }
                else
                {
                    OrigenOptions = new ObservableCollection<string>();
                    DestinoOptions = new ObservableCollection<string>();
                }

                IncotermOptions = new ObservableCollection<string>
                {
                    "EXW",
                    "FOB",
                    "DDP",
                    "CIF",
                    "CFR",
                    "DAP",
                    "FCA"
                };

                ServicioOptions = new ObservableCollection<string>
                {
                    "Marítimo LCL",
                    "Marítimo FCL 1x40STD/HC",
                    "Marítimo FCL 1x20STD",
                    "Aéreo",
                    "Carga Proyecto",
                    "Terrestre LTL",
                    "Terrestre FTL 1x53/48",
                    "Terrestre FTL 1x20"
                };

                ProductoOptions = new ObservableCollection<string>
                {
                    "Carga General",
                    "Carga Peligrosa",
                    "Alimentos",
                    "Menaje Casa"
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing data: {ex.Message}");
            }
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task EnviarCotizacion()
        {
            try
            {
                var contenido = $@"
                    Nombre Empresa: {NombreEmpresa}
                    Teléfono: {Telefono}
                    Correo: {Correo}
                    Incoterm: {SelectedIncotermOption}
                    Servicio: {SelectedServicioOption}
                    Origen: {SelectedOrigenOption}
                    Destino: {SelectedDestinoOption}
                    Tipo Producto: {SelectedProductoOption}
                    Valor Mercancía: {ValorMercancia}
                    Peso: {Peso}
                    Volumen: {Volumen}
                    Dimensiones: {Dimensiones}
                    Seguro de Carga: {(SeguroCarga ? "Sí" : "No")}
                    Transporte Nacional: {(TransporteNacional ? "Sí" : "No")}
                    Agencia Aduanas: {(AgenciaAduanas ? "Sí" : "No")}
                    Almacén Fiscal: {(AlmacenFiscal ? "Sí" : "No")}
                    Observaciones: {Observaciones}
                ";

                var emailData = new
                {
                    para = "pricing@grupocastrofallas.com",
                    asunto = "Solicitud de Cotización",
                    contenido = contenido
                };

                var jsonContent = JsonConvert.SerializeObject(emailData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://api.logisticacastrofallas.com/api/Mail/Send", content);

                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Éxito", "La cotización ha sido enviada.", "OK");
                        LimpiarCampos();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al enviar la cotización.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error sending email: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al enviar la cotización.", "OK");
            }
        }

        private void LimpiarCampos()
        {
            NombreEmpresa = string.Empty;
            Telefono = string.Empty;
            Correo = string.Empty;
            SelectedIncotermOption = null;
            SelectedServicioOption = null;
            SelectedOrigenOption = null;
            SelectedDestinoOption = null;
            SelectedProductoOption = null;
            ValorMercancia = string.Empty;
            Peso = string.Empty;
            Volumen = string.Empty;
            Dimensiones = string.Empty;
            Observaciones = string.Empty;
            SeguroCarga = false;
            TransporteNacional = false;
            AgenciaAduanas = false;
            AlmacenFiscal = false;
        }
        #endregion

        #region Comandos
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand EnviarCotizacionCommand => new Command(async () => await EnviarCotizacion());
        #endregion
    }
}