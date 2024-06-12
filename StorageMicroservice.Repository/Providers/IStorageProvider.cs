using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Repository.Providers
{
    public interface IStorageProvider
    {
        Task SaveFileAsync(string id, IFormFile file);
        Task<Stream> GetFileAsync(string id);
        Task DeleteFileAsync(string id);
    }
}
