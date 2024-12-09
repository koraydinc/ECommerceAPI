using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
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

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete($"{path}\\{fileName}");
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        {
            return File.Exists($"{path}\\{fileName}");
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();

            //List<bool> results = new List<bool>();

            foreach (IFormFile file in files)
            {
                //string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                string newFileName = await FileRenameAsync(uploadPath, file.Name, HasFile);
                await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
                datas.Add((file.Name, $"{path}\\{newFileName}"));
                //results.Add(result);
            }

            //if (results.TrueForAll(r => r.Equals(true)))
            //{
            //    return datas;
            //}

            return datas;
            //todo Eğer ki yukarıdaki if geçerli değilse burda dosyaların sisteme işlenmesi noktasında hata alındığını client'e bildir!
        }
    }
}
