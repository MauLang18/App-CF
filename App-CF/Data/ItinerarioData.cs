using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class ItinerarioData
    {
        public static async Task<ObservableCollection<ItinerarioModel>> ListItinerario(string cliente)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.logisticacastrofallas.com/api/Itinerario?Order=desc&Cliente={cliente}");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<ItinerarioModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<ItinerarioModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<ItinerarioModel> itinerarioModels = new ObservableCollection<ItinerarioModel>(
                            apiResponse.Data.Select(nd => new ItinerarioModel
                            {
                                Id = nd.Id,
                                Pol = nd.Pol,
                                Pod = nd.Pod,
                                Closing = nd.Closing,
                                Etd = nd.Etd,
                                Eta = nd.Eta,
                                Carrier = nd.Carrier,
                                Vessel = nd.Vessel,
                                Voyage = nd.Voyage,
                                Origen = nd.Origen,
                                Destino = nd.Destino,
                                Transporte = nd.Transporte,
                                Modalidad = nd.Modalidad,
                                FechaCreacionAuditoria = nd.FechaCreacionAuditoria,
                                Estado = nd.Estado,
                                EstadoItinerario = nd.EstadoItinerario
                            }));

                        return itinerarioModels;
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
    }
}