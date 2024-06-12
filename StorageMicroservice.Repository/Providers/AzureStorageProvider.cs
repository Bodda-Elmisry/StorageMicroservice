using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
    public class AzureStorageProvider : IStorageProvider
    {

        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient containerClient;

        public AzureStorageProvider(IOptions<AppConfigrations> appConfigrations)
        {
            blobServiceClient = new BlobServiceClient(appConfigrations.Value.AzureBlobStorageConnectionString);
            containerClient = blobServiceClient.GetBlobContainerClient(appConfigrations.Value.AzureBlobStorageContainerName);
        }

        public async Task SaveFileAsync(string id, IFormFile file)
        {
            try
            {
                var blobClient = containerClient.GetBlobClient(id);

                await using var stream = file.OpenReadStream();
                blobClient.Upload(stream);
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }
            catch (Exception ex) { }
        }

        public async Task<Stream?> GetFileAsync(string id)
        {
            var blobClient = containerClient.GetBlobClient(id);
            
            var blobExists = await blobClient.ExistsAsync();
            
            if (!blobExists) 
                return null;
            
            var response = await blobClient.DownloadAsync();
            
            return response.Value.Content;
        }

        public async Task DeleteFileAsync(string id)
        {
            var blobClient = containerClient.GetBlobClient(id);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
