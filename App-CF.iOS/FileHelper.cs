using App_CF.iOS;
using App_CF.Services;
using Foundation;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace App_CF.iOS
{
    public class FileHelper : IFileHelper
    {
        public async Task<string> ReadTextFileAsync(string filename)
        {
            string path = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename));

            using (StreamReader sr = new StreamReader(path))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}