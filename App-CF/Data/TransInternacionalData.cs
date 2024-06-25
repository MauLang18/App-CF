using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        static TransInternacionalData()
        {
            CantEquipo = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("cantEquipo.json"));
            Destino = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("destino.json"));
            Incoterm = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("incoterm.json"));
            Origen = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("origen.json"));
            Poe = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("poe.json"));
            Pol = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("pol.json"));
            Status = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("status.json"));
            TamanoEquipo = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("tamanoEquipo.json"));
            Transporte = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("transporte.json"));
        }

        public static async Task<ObservableCollection<TransInternacionalModel>> ListTransInternacional(string cliente)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.logisticacastrofallas.com/api/TrackingLogin/Activo?cliente={cliente}");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<DataTransInternacional> apiResponse = JsonConvert.DeserializeObject<ApiResponse<DataTransInternacional>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<TransInternacionalModel> transInternacionalModels = new ObservableCollection<TransInternacionalModel>(
                            apiResponse.Data.SelectMany(nd => nd.value).Select(nd => new TransInternacionalModel
                            {
                                odataetag = nd.odataetag,
                                new_contenedor = nd.new_contenedor,
                                new_factura = nd.new_factura,
                                new_bcf = nd.new_bcf,
                                new_cantequipo = CantEquipoMapping(nd.new_cantequipo.ToString()),
                                new_commodity = nd.new_commodity,
                                new_confirmacinzarpe = nd.new_confirmacinzarpe,
                                new_contidadbultos = nd.new_contidadbultos,
                                new_destino = DestinoMapping(nd.new_destino.ToString()),
                                new_eta = nd.new_eta,
                                new_etd1 = nd.new_etd1,
                                modifiedon = nd.modifiedon,
                                new_incoterm = IncotermMapping(nd.new_incoterm.ToString()),
                                new_origen = OrigenMapping(nd.new_origen.ToString()),
                                new_po = nd.new_po,
                                new_poe = PoeMapping(nd.new_poe.ToString()),
                                new_pol = PolMapping(nd.new_pol.ToString()),
                                new_preestado2 = StatusMapping(nd.new_preestado2.ToString()),
                                new_seal = nd.new_seal,
                                _new_shipper_value = nd._new_shipper_value,
                                new_statuscliente = nd.new_statuscliente,
                                new_tamaoequipo = TamanoEquipoMapping(nd.new_tamaoequipo.ToString()),
                                new_new_facturacompaia = nd.new_new_facturacompaia,
                                new_transporte = TransporteMapping(nd.new_transporte.ToString()),
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

        private static string CantEquipoMapping(string dato)
        {
            if (CantEquipo.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string DestinoMapping(string dato)
        {
            if (Destino.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string IncotermMapping(string dato)
        {
            if (Incoterm.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string OrigenMapping(string dato)
        {
            if (Origen.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string PoeMapping(string dato)
        {
            if (Poe.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string PolMapping(string dato)
        {
            if (Pol.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string StatusMapping(string dato)
        {
            if (Status.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string TamanoEquipoMapping(string dato)
        {
            if (TamanoEquipo.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }

        private static string TransporteMapping(string dato)
        {
            if (Transporte.TryGetValue(dato, out string valor))
            {
                return valor;
            }
            return dato;
        }
    }
}