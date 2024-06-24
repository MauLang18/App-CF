using App_CF.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class DirecInternoPageViewModel : BaseViewModel
    {
        #region VARIABLES
        ObservableCollection<DirecInternoModel> _DirectorioInterno;
        #endregion

        #region CONSTRUCTOR
        public DirecInternoPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Mostrar();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<DirecInternoModel> DirectorioInterno
        {
            get { return _DirectorioInterno; }
            set
            {
                SetValue(ref _DirectorioInterno, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public void Mostrar()
        {
            DirectorioInterno = GetDirectorio();
        }

        private ObservableCollection<DirecInternoModel> GetDirectorio()
        {
            return new ObservableCollection<DirecInternoModel>
            {
                new DirecInternoModel { Puesto = "General Manager", Nombre = "Carl Jensen", Numero = "Ofic. +506 2272-6772", Correo = "cjenseng@castrofallas.com"},
                new DirecInternoModel { Puesto = "Operations Manager", Nombre = "Cesar Mesen", Numero = "Ofic. +506 2272-6772", Correo = "cemesen@castrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Manager", Nombre = "Royner Sibaja", Numero = "Ofic. +506 2272-6772 Ext. 150\r\nWhatsApp. +506 7078-6941", Correo = "rsibaja@castrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Operations Manager", Nombre = "Josue Alvarado", Numero = "Ofic. +506 2272-6772 Ext. 134\r\nWhatsApp. +506 6119-6970", Correo = "logistica2@castrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Finance Manager", Nombre = "Sonia Quiros", Numero = "Ofic. +506 2272-6772 Ext. 104\r\nWhatsApp. +506 6283-8475", Correo = "squiros@castrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Executive Panama", Nombre = "Danna Salas", Numero = "Ofic. +506 2272-6772 Ext. 122\r\nWhatsApp. +506 6354-1392", Correo = "panama@grupocastrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Executive Costa Rica", Nombre = "Gabriel Miranda", Numero = "Ofic. +506 2272-6772 Ext. 151\r\nWhatsApp. +506 7005 1261", Correo = "costarica@grupocastrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Executive Coordination Origen", Nombre = "Genesis Tenorio", Numero = "Ofic. +506 2272-6772 Ext. 124\r\nWhatsApp. +506 7005 1261", Correo = "china@grupocastrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Executive CentroAmerica and Mexico", Nombre = "Andrea Monge", Numero = "Ofic. +506 2272-6772 Ext. 113\r\nWhatsApp. +506 6159 5496", Correo = "guatemala@grupocastrofallas.com"},
                new DirecInternoModel { Puesto = "Logistics Executive USA and Canada", Nombre = "Hilary Fernandez", Numero = "Ofic. +506 2272-6772 Ext. 113\r\nWhatsApp. +506 6354 3767", Correo = "usa@grupocastrofallas.com"},
                new DirecInternoModel { Puesto = "Pricing", Nombre = "Maria Fonseca", Numero = "Ofic. +506 2272-6772 Ext. 135\r\nWhatsApp. +506 7256 5044", Correo = "pricing@grupocastrofallas.com"},
                new DirecInternoModel { Puesto = "Commercial Executive", Nombre = "Pablo Miranda", Numero = "Ofic. +506 2272-6772 Ext. 133\r\nWhatsApp. +506 6339 0028", Correo = "comercial1@castrofallas.com"},
                new DirecInternoModel { Puesto = "Customer service", Nombre = "Customer service", Numero = "Ofic. +506 2272-6772 Ext. 129 / 143\r\nWhatsApp. +506 7006 3878", Correo = "servicioalcliente@castrofallas.com"},
                new DirecInternoModel { Puesto = "Billing", Nombre = "Fabian Villalobos", Numero = "Ofic. +506 2272-6772 Ext. 141\r\nWhatsApp. +506 8354 0875", Correo = "fvillalobos@castrofallas.com"},
                new DirecInternoModel { Puesto = "Fiscal WareHouse", Nombre = "Jaime Pacheco", Numero = "Ofic. +506 2272-6772 Ext. 103 / 100", Correo = "facturacion1@castrofallas.com"},
                new DirecInternoModel { Puesto = "Sales Manager", Nombre = "Richard Soto", Numero = "Ofic. +506 2272-6772 Ext. 142\r\nWhatsApp. +506 7078 6893", Correo = "rsoto@castrofallas.com"},
                new DirecInternoModel { Puesto = "Commercial Executive", Nombre = "Stephanie Redondo", Numero = "Ofic. +506 2272-6772 Ext. 123\r\nWhatsApp. +506 6434 6756", Correo = "sredondo@castrofallas.com"},
                new DirecInternoModel { Puesto = "Commercial Executive", Nombre = "Dennis Acuna", Numero = "Ofic. +506 2272-6772 Ext. 142\r\nWhatsApp. +506 7078 6860", Correo = "dacuna@castrofallas.com"},
                new DirecInternoModel { Puesto = "Commercial Executive", Nombre = "Rafael Montero", Numero = "Ofic. +506 2272-6772 Ext. 142\r\nWhatsApp. +506 7208 3068", Correo = "rmontero@castrofallas.com"},
                new DirecInternoModel { Puesto = "Finance Manager", Nombre = "Oscar Chavarria", Numero = "Ofic. +506 2272-6772 Ext. 114", Correo = "ochavarria@castrofallas.com"},
                new DirecInternoModel { Puesto = "Accounts Receivable", Nombre = "Tere Perez", Numero = "Ofic. +506 2272-6772 Ext. 114", Correo = "estados1@castrofallas.com"},
            };
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        #endregion
    }
}