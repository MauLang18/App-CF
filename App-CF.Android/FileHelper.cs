using Android.App;
using Android.Content.Res;
using App_CF.Droid;
using App_CF.Services;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace App_CF.Droid
{
    public class FileHelper : IFileHelper
    {
        public async Task<string> ReadTextFileAsync(string filename)
        {
            AssetManager assets = Application.Context.Assets;
            using (StreamReader sr = new StreamReader(assets.Open(filename)))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}