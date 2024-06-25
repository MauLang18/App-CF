using System.Threading.Tasks;

namespace App_CF.Services
{
    public interface IFileHelper
    {
        Task<string> ReadTextFileAsync(string filename);
    }
}