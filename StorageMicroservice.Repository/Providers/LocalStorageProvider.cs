using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StorageMicroservice.Repository.Configrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StorageMicroservice.Repository.Providers
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly AppConfigrations appConfigrations;

        public LocalStorageProvider(AppConfigrations appConfigrations)
        {
            this.appConfigrations = appConfigrations;
        }

        public async Task DeleteFileAsync(string id)
        {
            var filePath = Path.Combine(appConfigrations.LocalStoragePath, id);
            
            if (File.Exists(filePath)) 
                File.Delete(filePath);
        }

        public async Task<Stream> GetFileAsync(string id)
        {
            var filePath = Path.Combine(appConfigrations.LocalStoragePath, id);

            if (!File.Exists(filePath)) 
                return null;

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        public async Task SaveFileAsync(string id, IFormFile file)
        {
            var filePath = Path.Combine(appConfigrations.LocalStoragePath, id);

            using var stream = new FileStream(filePath, FileMode.Create);
            
            await file.CopyToAsync(stream);
        }

    }
}
