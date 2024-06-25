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
    public class TransInternacionalData
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

        public TransInternacionalData()
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

        public static async Task<ObservableCollection<TransInternacionalModel>> ListTransInternacional(string cliente)
        {
            try
            {
                await EnsureDictionariesLoaded();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.logisticacastrofallas.com/api/TrackingLogin/Activo?cliente={cliente}");
                    response.EnsureSuccessStatusCode();
                    string resultado = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse2<TransInternacionalModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<TransInternacionalModel> transInternacionalModels = new ObservableCollection<TransInternacionalModel>(
                            apiResponse.Data.Value.Select(nd => new TransInternacionalModel
                            {
                                odataetag = nd.odataetag,
                                new_contenedor = nd.new_contenedor,
                                new_factura = nd.new_factura,
                                new_bcf = nd.new_bcf,
                                new_cantequipo = GetCantEquipoValue(nd.new_cantequipo.ToString()),
                                new_commodity = nd.new_commodity,
                                new_confirmacinzarpe = nd.new_confirmacinzarpe,
                                new_contidadbultos = nd.new_contidadbultos,
                                new_destino = GetDestinoValue(nd.new_destino.ToString()),
                                new_eta = nd.new_eta,
                                new_etd1 = nd.new_etd1,
                                modifiedon = nd.modifiedon,
                                new_incoterm = GetIncotermValue(nd.new_incoterm.ToString()),
                                new_origen = GetOrigenValue(nd.new_origen.ToString()),
                                new_po = nd.new_po,
                                new_poe = GetPoeValue(nd.new_poe.ToString()),
                                new_pol = GetPolValue(nd.new_pol.ToString()),
                                new_preestado2 = GetStatusValue(nd.new_preestado2.ToString()),
                                new_seal = nd.new_seal,
                                _new_shipper_value = nd._new_shipper_value,
                                new_statuscliente = nd.new_statuscliente,
                                new_tamaoequipo = GetTamanoEquipoValue(nd.new_tamaoequipo.ToString()),
                                new_new_facturacompaia = nd.new_new_facturacompaia,
                                new_transporte = GetTransporteValue(nd.new_transporte.ToString()),
                                title = nd.title,
                                new_lugarcolocacion = nd.new_lugarcolocacion,
                                new_redestino = nd.new_redestino,
                                new_diasdetransito = nd.new_diasdetransito,
                                new_barcodesalida = nd.new_barcodesalida,
                                new_viajedesalida = nd.new_viajedesalida,
                                _customerid_value = nd._customerid_value,
                                new_ofertatarifaid = nd.new_ofertatarifaid,
                                new_proyecciondeingreso = nd.new_proyecciondeingreso,
                                incidentid = nd.incidentid
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
                await new TransInternacionalData().LoadDictionariesAsync();
            }
        }
    }
}
