using App_CF.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_CF.ViewModel
{
    public class WHSDetailPageViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Imagen1;
        string _Imagen2;
        string _Imagen3;
        string _Imagen4;
        string _Imagen5;
        string _WhsReceipt;
        string _Documentoregistro;
        string _WhsReceiptURL;
        string _DocumentoregistroURL;
        string _MainImage;
        #endregion

        #region CONSTRUCTOR
        public WHSDetailPageViewModel(INavigation navigation, WHSModel model)
        {
            Navigation = navigation;

            Imagen1 = model.Imagen1;
            Imagen2 = model.Imagen2;
            Imagen3 = model.Imagen3;
            Imagen4 = model.Imagen4;
            Imagen5 = model.Imagen5;
            MainImage = Imagen1;
            Documentoregistro = $"https://docs.google.com/viewer?url={model.Documentoregistro}";
            DocumentoregistroURL = model.Documentoregistro;
            WhsReceipt = $"https://docs.google.com/viewer?url={model.WhsReceipt}";
            WhsReceiptURL = model.WhsReceipt;
        }
        #endregion

        #region OBJETOS
        public string Imagen1
        {
            get => _Imagen1;
            set
            {
                _Imagen1 = value;
                OnPropertyChanged(nameof(Imagen1));
            }
        }

        public string Imagen2
        {
            get => _Imagen2;
            set
            {
                _Imagen2 = value;
                OnPropertyChanged(nameof(Imagen2));
            }
        }

        public string Imagen3
        {
            get => _Imagen3;
            set
            {
                _Imagen3 = value;
                OnPropertyChanged(nameof(Imagen3));
            }
        }

        public string Imagen4
        {
            get => _Imagen4;
            set
            {
                _Imagen4 = value;
                OnPropertyChanged(nameof(Imagen4));
            }
        }

        public string Imagen5
        {
            get => _Imagen5;
            set
            {
                _Imagen5 = value;
                OnPropertyChanged(nameof(Imagen5));
            }
        }

        public string WhsReceipt
        {
            get => _WhsReceipt;
            set
            {
                _WhsReceipt = value;
                OnPropertyChanged(nameof(WhsReceipt));
            }
        }

        public string Documentoregistro
        {
            get => _Documentoregistro;
            set
            {
                _Documentoregistro = value;
                OnPropertyChanged(nameof(Documentoregistro));
            }
        }

        public string WhsReceiptURL
        {
            get => _WhsReceiptURL;
            set
            {
                _WhsReceiptURL = value;
                OnPropertyChanged(nameof(WhsReceiptURL));
            }
        }

        public string DocumentoregistroURL
        {
            get => _DocumentoregistroURL;
            set
            {
                _DocumentoregistroURL = value;
                OnPropertyChanged(nameof(DocumentoregistroURL));
            }
        }

        public string MainImage
        {
            get => _MainImage;
            set
            {
                _MainImage = value;
                OnPropertyChanged(nameof(MainImage));
            }
        }
        #endregion

        #region PROCESOS
        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public void ChangeImage(string imageUrl)
        {
            MainImage = imageUrl;
        }

        public void OpenUrlWhsReceipt()
        {
            Launcher.OpenAsync(new Uri(WhsReceiptURL));
        }

        public void OpenUrlDocumentoregistro()
        {
            Launcher.OpenAsync(new Uri(DocumentoregistroURL));
        }
        #endregion

        #region COMANDOS
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand ChangeImageCommand => new Command<string>((imageUrl) => ChangeImage(imageUrl));
        public ICommand WhsReceiptCommand => new Command(OpenUrlWhsReceipt);
        public ICommand DocumentoregistroCommand => new Command(OpenUrlDocumentoregistro);
        #endregion
    }
}