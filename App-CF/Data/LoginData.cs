using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class LoginData
    {
        public static async Task<ApiResponseToken> Login(string correo, string pass)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestBody = new { correo, pass };
                    var jsonRequest = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://api.logisticacastrofallas.com/api/Auth/Login?authType=Interno", content);

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponseToken apiResponse = JsonConvert.DeserializeObject<ApiResponseToken>(resultado);

                    return apiResponse;
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
