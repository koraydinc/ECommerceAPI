using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);

            string oldName = Path.GetFileNameWithoutExtension(fileName);            

            string regulatedFileName = NameOperation.CharacterRegulatory(oldName);

            string date = DateTime.Now.ToString("yyyyMMddHHmmss");

            string newFileName = $"{regulatedFileName}-{date}{extension}";

            if (File.Exists($"{path}\\{newFileName}"))
            {
                return await FileRenameAsync(path, newFileName);
            }
            else
            {
                return newFileName;
            }
        }
    }
}
