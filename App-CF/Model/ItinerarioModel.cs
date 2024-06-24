using System;

namespace App_CF.Model
{
    public class ItinerarioModel
    {
        public int Id { get; set; }
        public string Pol { get; set; }
        public string Pod { get; set; }
        public DateTime Closing { get; set; }
        public DateTime Etd { get; set; }
        public DateTime Eta { get; set; }
        public string Carrier { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Transporte { get; set; }
        public string Modalidad { get; set; }
        public DateTime FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string EstadoItinerario { get; set; }
    }
}