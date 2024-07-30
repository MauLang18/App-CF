using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace App_CF.View
{
    public partial class CotizacionPage : ContentPage
    {
        public CotizacionPage()
        {
            InitializeComponent();
        }

        private async void OnEnviarCotizacionClicked(object sender, EventArgs e)
        {
            var emailBody = $@"
                <h1>Cotización de Servicios</h1>
                <p><strong>Nombre Empresa:</strong> {NombreEntry.Text}</p>
                <p><strong>Teléfono:</strong> {TelefonoEntry.Text}</p>
                <p><strong>Correo:</strong> {CorreoEntry.Text}</p>
                <p><strong>Operación:</strong> {(ImportacionCheckBox.IsChecked ? "Importación" : "")}{(ExportacionCheckBox.IsChecked ? ", Exportación" : "")}</p>
                <p><strong>Negociación:</strong> {NegociacionPicker.SelectedItem}</p>
                <p><strong>Servicio:</strong> {ServicioPicker.SelectedItem}</p>
                <p><strong>Origen:</strong> {OrigenPicker.SelectedItem}</p>
                <p><strong>Destino:</strong> {DestinoPicker.SelectedItem}</p>
                <p><strong>Tipo Producto:</strong> {TipoProductoEntry.Text}</p>
                <p><strong>Valor Mercancía:</strong> {ValorMercanciaEntry.Text}</p>
                <p><strong>Servicios Integrales:</strong> {(SeguroCargaCheckBox.IsChecked ? "Seguro de Carga, " : "")}{(TransporteNacionalCheckBox.IsChecked ? "Transporte Nacional, " : "")}{(AgenciaAduanasCheckBox.IsChecked ? "Agencia Aduanas, " : "")}{(AlmacenFiscalCheckBox.IsChecked ? "Almacén Fiscal" : "")}</p>
                <p><strong>Peso:</strong> {PesoEntry.Text}</p>
                <p><strong>Volumen:</strong> {VolumenEntry.Text}</p>
                <p><strong>Dimensiones:</strong> {DimensionesEntry.Text}</p>
                <p><strong>Observaciones:</strong> {ObservacionesEditor.Text}</p>
            ";

            var emailContent = new
            {
                para = "pricing@grupocastrofallas.com",
                asunto = "Cotización",
                contenido = emailBody
            };

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(emailContent);
                    var response = await httpClient.PostAsync("https://api.logisticacastrofallas.com/api/Mail/Send",
                        new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Cotización enviada con éxito!", "OK");
                        NombreEntry.Text = "";
                        TelefonoEntry.Text = "";
                        CorreoEntry.Text = "";
                        ImportacionCheckBox.IsChecked = false;
                        ExportacionCheckBox.IsChecked = false;
                        NegociacionPicker.SelectedIndex = -1;
                        ServicioPicker.SelectedIndex = -1;
                        OrigenPicker.SelectedIndex = -1;
                        DestinoPicker.SelectedIndex = -1;
                        TipoProductoEntry.Text = "";
                        ValorMercanciaEntry.Text = "";
                        SeguroCargaCheckBox.IsChecked = false;
                        TransporteNacionalCheckBox.IsChecked = false;
                        AgenciaAduanasCheckBox.IsChecked = false;
                        AlmacenFiscalCheckBox.IsChecked = false;
                        PesoEntry.Text = "";
                        VolumenEntry.Text = "";
                        DimensionesEntry.Text = "";
                        ObservacionesEditor.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al enviar la cotización: " + response.ReasonPhrase, "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error al enviar el formulario: " + ex.Message, "OK");
                }
            }
        }
    }
}