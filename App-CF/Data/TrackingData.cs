using App_CF.Model;
using App_CF.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_CF.Data
{
    public class TrackingData
    {
        private static Dictionary<string, string> CantEquipo;
        private static Dictionary<string, string> Destino;
        private static Dictionary<string, string> Incoterm;
        private static Dictionary<string, string> Origen;
        private static Dictionary<string, string> Poe;
        private static Dictionary<string, string> Pol;
        private static Dictionary<string, string> Status;
        private static Dictionary<string, string> TamanoEquipo;
        private static Dictionary<string, string> Transporte;

        private static bool dictionariesLoaded = false;

        public TrackingData()
        {
            LoadDictionariesAsync();
        }

        private async Task LoadDictionariesAsync()
        {
            try
            {
                if (!dictionariesLoaded)
                {
                    CantEquipo = await LoadDictionaryAsync("cantEquipo.json");
                    Destino = await LoadDictionaryAsync("destino.json");
                    Incoterm = await LoadDictionaryAsync("incoterm.json");
                    Origen = await LoadDictionaryAsync("origen.json");
                    Poe = await LoadDictionaryAsync("poe.json");
                    Pol = await LoadDictionaryAsync("pol.json");
                    Status = await LoadDictionaryAsync("status.json");
                    TamanoEquipo = await LoadDictionaryAsync("tamanoEquipo.json");
                    Transporte = await LoadDictionaryAsync("transporte.json");

                    dictionariesLoaded = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al cargar los diccionarios: {ex.Message}", ex);
            }
        }

        private async Task<Dictionary<string, string>> LoadDictionaryAsync(string fileName)
        {
            string json = await DependencyService.Get<IFileHelper>().ReadTextFileAsync(fileName);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static async Task<ObservableCollection<TrackingModel>> ListTracking(string cliente, string searchText, string searchOption)
        {
            try
            {
                await EnsureDictionariesLoaded();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.logisticacastrofallas.com/api/LoginTracking/{searchOption}?{searchOption}={searchText}&cliente={cliente}");
                    response.EnsureSuccessStatusCode();
                    string resultado = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse2<TrackingModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<TrackingModel> transInternacionalModels = new ObservableCollection<TrackingModel>(
                            apiResponse.Data.Value.Select(nd => new TrackingModel
                            {
                                odataetag = nd.odataetag,
                                new_contenedor = nd.new_contenedor,
                                new_factura = nd.new_factura,
                                new_bcf = nd.new_bcf,
                                new_commodity = nd.new_commodity,
                                new_confirmacinzarpe = nd.new_confirmacinzarpe,
                                new_contidadbultos = nd.new_contidadbultos,
                                new_destino = GetDestinoValue(nd.new_destino.ToString()),
                                new_eta = nd.new_eta,
                                new_etd1 = nd.new_etd1,
                                modifiedon = nd.modifiedon,
                                new_origen = GetOrigenValue(nd.new_origen.ToString()),
                                new_po = nd.new_po,
                                new_poe = GetPoeValue(nd.new_poe.ToString()),
                                new_pol = GetPolValue(nd.new_pol.ToString()),
                                new_preestado2 = GetStatusValue(nd.new_preestado2.ToString()),
                                _new_shipper_value = nd._new_shipper_value,
                                new_statuscliente = nd.new_statuscliente,
                                new_transporte = GetTransporteValue(nd.new_transporte.ToString()),
                                title = nd.title,
                                new_barcodesalida = nd.new_barcodesalida,
                                new_instcliente = nd.new_instcliente,
                                new_etadestino = nd.new_etadestino,
                                new_fechayhoraoficializacion = nd.new_fechayhoraoficializacion,
                                new_ingreso = nd.new_ingreso,
                                new_ingresoabodegas = nd.new_ingresoabodegas,
                                TimelineEvents = new ObservableCollection<TimelineEvent>
                                {
                            new TimelineEvent
                            {
                                Date = "ETD",
                                Label = $"ETD: {FormatDate(nd.new_etd1)}",
                                Description = $"POL: {GetPolValue(nd.new_pol.ToString())}"
                            },
                            new TimelineEvent
                            {
                                Date = "Confirmación Zarpe",
                                Label = $"BUQUE: {nd.new_barcodesalida}",
                                Description = $"ZARPE: {FormatDate(nd.new_confirmacinzarpe)}"
                            },
                            new TimelineEvent
                            {
                                Date = "Notif. Aviso Arribo",
                                Label = $"ARRIBO: {FormatDate(nd.new_instcliente)}"
                            },
                            new TimelineEvent
                            {
                                Date = "ETA",
                                Label = $"ETA: {FormatDate(nd.new_eta)}",
                                Description = $"POE: {GetPoeValue(nd.new_poe.ToString())}"
                            },
                            new TimelineEvent
                            {
                                Date = "ETA DESTINO",
                                Label = $"ETA: {FormatDate(nd.new_etadestino)}",
                                Description = $"POD: {GetDestinoValue(nd.new_destino.ToString())}"
                            },
                            new TimelineEvent
                            {
                                Date = "Confirmacion de Arribo",
                                Label = $"OFICIALIZACION: {FormatDateTime(nd.new_fechayhoraoficializacion)}"
                            },
                            new TimelineEvent
                            {
                                Date = "ENTREGA",
                                Label = $"ENTREGA: {FormatDate(nd.new_ingreso) ?? FormatDate(nd.new_ingresoabodegas)}"
                            }
                                }
                            }));

                        return transInternacionalModels;
                    }
                    else
                    {
                        string mensaje = apiResponse != null ? apiResponse.Message : "Error desconocido en la respuesta.";
                        throw new ApplicationException(mensaje);
                    }
                }
            }
            catch (HttpRequestException hrex)
            {
                throw new ApplicationException("Error al realizar la solicitud HTTP: " + hrex.Message, hrex);
            }
            catch (JsonException jex)
            {
                throw new ApplicationException("Error al deserializar la respuesta JSON: " + jex.Message, jex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Se produjo una excepción: " + ex.Message, ex);
            }
        }

        private static string FormatDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime formattedDate))
            {
                return formattedDate.ToString("yyyy-MM-dd");
            }
            return date;
        }

        private static string FormatDateTime(string dateTime)
        {
            if (DateTime.TryParse(dateTime, out DateTime formattedDateTime))
            {
                return formattedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return dateTime;
        }


        private static string GetDestinoValue(string dato)
        {
            if (Destino.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetIncotermValue(string dato)
        {
            if (Incoterm.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetOrigenValue(string dato)
        {
            if (Origen.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetPoeValue(string dato)
        {
            if (Poe.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetPolValue(string dato)
        {
            if (Pol.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetStatusValue(string dato)
        {
            if (Status.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetTamanoEquipoValue(string dato)
        {
            if (TamanoEquipo.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        private static string GetTransporteValue(string dato)
        {
            if (Transporte.TryGetValue(dato, out string valor))
                return valor;
            return dato;
        }

        public static string GetCantEquipoValue(string key)
        {
            if (CantEquipo != null && CantEquipo.ContainsKey(key))
                return CantEquipo[key];
            else
                return key;
        }

        private static async Task EnsureDictionariesLoaded()
        {
            if (!dictionariesLoaded)
            {
                await new TrackingData().LoadDictionariesAsync();
            }
        }
    }
}
