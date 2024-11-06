using ECommerceAPI.Application.Services;
using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService : IFileService
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

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception e)
            {
                //todo log!
                throw e;
            }

        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> values = new List<(string fileName, string path)>();

            List<bool> results = new List<bool>();

            foreach (IFormFile file in formFiles)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                values.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
            {
                return values;
            }

            return null;
            //todo Eğer ki yukarıdaki if geçerli değilse burda dosyaların sisteme işlenmesi noktasında hata alındığını client'e bildir!
        }
    }
}
