using App_CF.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class LoginData
    {
        public static async Task<ApiResponseToken> Login(string correo, string pass)
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    correo,
                    pass
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.logisticacastrofallas.com/api/Auth/Login?authType=Interno", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    ApiResponseToken apiResponse = JsonConvert.DeserializeObject<ApiResponseToken>(responseContent);
                    return apiResponse;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}