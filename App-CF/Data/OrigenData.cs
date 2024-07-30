using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class OrigenData
    {
        public static async Task<ObservableCollection<PuertoModel>> GetFilteredWhs()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.logisticacastrofallas.com/api/Pol/Whs");
                    response.EnsureSuccessStatusCode();
                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<PuertoModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<PuertoModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        var filteredWhs = apiResponse.Data.Where(w => w.Whs == 1).ToList();
                        return new ObservableCollection<PuertoModel>(filteredWhs);
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

        public static async Task<ObservableCollection<PaisModel>> GetPaises()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.logisticacastrofallas.com/api/Origen/Select");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaisModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        return new ObservableCollection<PaisModel>(apiResponse.Data);
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