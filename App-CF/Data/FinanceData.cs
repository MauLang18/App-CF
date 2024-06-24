using App_CF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App_CF.Data
{
    public class FinanceData
    {
        public static async Task<ObservableCollection<FinanceModel>> ListFinance(string cliente)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.logisticacastrofallas.com/api/Finance/Cliente?Order=desc&Cliente={cliente}");

                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();

                    ApiResponse<FinanceModel> apiResponse = JsonConvert.DeserializeObject<ApiResponse<FinanceModel>>(resultado);

                    if (apiResponse != null && apiResponse.IsSuccess)
                    {
                        ObservableCollection<FinanceModel> financeModels = new ObservableCollection<FinanceModel>(
                            apiResponse.Data.Select(nd => new FinanceModel
                            {
                                Id = nd.Id,
                                Cliente = nd.Cliente,
                                NombreCliente = nd.NombreCliente,
                                EstadoCuenta = nd.EstadoCuenta,
                                FechaCreacionAuditoria = nd.FechaCreacionAuditoria,
                                Estado = nd.Estado,
                                EstadoFinance = nd.EstadoFinance
                            }));

                        return financeModels;
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