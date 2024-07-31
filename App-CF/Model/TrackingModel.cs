using System.Collections.ObjectModel;

namespace App_CF.Model
{
    public class TrackingModel
    {
        public string odataetag { get; set; }
        public string title { get; set; }
        public string new_bcf { get; set; }
        public string new_contenedor { get; set; }
        public string new_po { get; set; }
        public string new_factura { get; set; }
        public string _new_shipper_value { get; set; }
        public string new_preestado2 { get; set; }
        public string new_statuscliente { get; set; }
        public string modifiedon { get; set; }
        public string new_origen { get; set; }
        public string new_poe { get; set; }
        public string new_pol { get; set; }
        public string new_destino { get; set; }
        public string new_peso { get; set; }
        public string new_transporte { get; set; }
        public string new_commodity { get; set; }
        public string new_contidadbultos { get; set; }
        public string new_ingresoabodegas { get; set; }
        public string new_ingreso { get; set; }
        public string new_fechayhoraoficializacion { get; set; }
        public string new_etadestino { get; set; }
        public string new_eta { get; set; }
        public string new_instcliente { get; set; }
        public string new_confirmacinzarpe { get; set; }
        public string new_barcodesalida { get; set; }
        public string new_etd1 { get; set; }
        public ObservableCollection<TimelineEvent> TimelineEvents { get; set; }
    }
}