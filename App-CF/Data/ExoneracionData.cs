using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class ExoneracionData
    {
        public static async Task<ObservableCollection<ExoneracionModel>> ListExoneracion(string cliente)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.logisticacastrofallas.com/api/Exoneracion?Order=desc&Cliente={cliente}");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<ExoneracionModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<ExoneracionModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<ExoneracionModel> exoneracionModels = new ObservableCollection<ExoneracionModel>(
                            apiResponse.Data.Select(nd => new ExoneracionModel
                            {
                                Id = nd.Id,
                                Idtra = nd.Idtra,
                                Cliente = nd.Cliente,
                                NombreCliente = nd.NombreCliente,
                                TipoExoneracion = nd.TipoExoneracion,
                                StatusExoneracion = nd.StatusExoneracion,
                                Producto = nd.Producto,
                                Categoria = nd.Categoria,
                                ClasificacionArancelaria = nd.ClasificacionArancelaria,
                                NumeroSolicitud = nd.NumeroSolicitud,
                                Solicitud = nd.Solicitud,
                                NumeroAutorizacion = nd.NumeroAutorizacion,
                                Autorizacion = nd.Autorizacion,
                                Desde = nd.Desde,
                                Hasta = nd.Hasta,
                                Descripcion = nd.Descripcion,
                                FechaCreacionAuditoria = nd.FechaCreacionAuditoria,
                                Estado = nd.Estado,
                                EstadoExoneracion = nd.EstadoExoneracion
                            }));

                        return exoneracionModels;
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