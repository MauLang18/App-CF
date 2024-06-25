using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class WHSData
    {
        public static async Task<ObservableCollection<WHSModel>> ListWHS(string cliente, string whs)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.logisticacastrofallas.com/api/Whs/Cliente?Order=Desc&cliente={cliente}&whs={whs}");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<WHSModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<WHSModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<WHSModel> whsModels = new ObservableCollection<WHSModel>(
                            apiResponse.Data.Select(nd => new WHSModel
                            {
                                Id = nd.Id,
                                Idtra = nd.Idtra,
                                NumeroWHS = nd.NumeroWHS,
                                Cliente = nd.Cliente,
                                NombreCliente = nd.NombreCliente,
                                TipoRegistro = nd.TipoRegistro,
                                StatusWHS = nd.StatusWHS,
                                Po = nd.Po,
                                Pol = nd.Pol,
                                Pod = nd.Pod,
                                CantidadBultos = nd.CantidadBultos,
                                TipoBultos = nd.TipoBultos,
                                VinculacionOtroRegistro = nd.VinculacionOtroRegistro,
                                WhsReceipt = nd.WhsReceipt,
                                Documentoregistro = nd.Documentoregistro,
                                Imagen1 = nd.Imagen1,
                                Imagen2 = nd.Imagen2,
                                Imagen3 = nd.Imagen3,
                                Imagen4 = nd.Imagen4,
                                Imagen5 = nd.Imagen5,
                                Detalle = nd.Detalle,
                                FechaCreacionAuditoria = nd.FechaCreacionAuditoria,
                                Estado = nd.Estado,
                                EstadoWhs = nd.EstadoWhs
                            }));

                        return whsModels;
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