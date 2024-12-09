using ECommerceAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFile)
        {
            string extension = Path.GetExtension(fileName);

            string oldName = Path.GetFileNameWithoutExtension(fileName);

            string regulatedFileName = NameOperation.CharacterRegulatory(oldName);

            string date = DateTime.Now.ToString("yyyyMMddHHmmss");

            string newFileName = $"{regulatedFileName}-{date}{extension}";

            if (hasFile(pathOrContainerName, newFileName))
            {
                return await FileRenameAsync(pathOrContainerName, newFileName, hasFile);
            }
            else
            {
                return newFileName;
            }
        }
    }
}
