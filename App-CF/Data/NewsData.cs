using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class NewsData
    {
        public static async Task<ObservableCollection<NewsModel>> ListNews()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.logisticacastrofallas.com/api/Noticia?Order=desc");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<NewsModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<NewsModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<NewsModel> newsModels = new ObservableCollection<NewsModel>(
                            apiResponse.Data.Select(nd => new NewsModel
                            {
                                Id = nd.Id,
                                Titulo = nd.Titulo,
                                Subtitulo = nd.Subtitulo,
                                Contenido = nd.Contenido,
                                Imagen = nd.Imagen,
                                FechaCreacionAuditoria = nd.FechaCreacionAuditoria,
                                Estado = nd.Estado,
                                EstadoNoticia = nd.EstadoNoticia
                            }));

                        return newsModels;
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

        public static async Task<ObservableCollection<NewsModel>> ListLatestNews()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.logisticacastrofallas.com/api/Noticia?Order=desc&Records=5");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<NewsModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<NewsModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<NewsModel> newsModels = new ObservableCollection<NewsModel>(
                            apiResponse.Data.Select(nd => new NewsModel
                            {
                                Id = nd.Id,
                                Titulo = nd.Titulo,
                                Subtitulo = nd.Subtitulo,
                                Contenido = nd.Contenido,
                                Imagen = nd.Imagen,
                                FechaCreacionAuditoria = nd.FechaCreacionAuditoria,
                                Estado = nd.Estado,
                                EstadoNoticia = nd.EstadoNoticia
                            }));

                        return newsModels;
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