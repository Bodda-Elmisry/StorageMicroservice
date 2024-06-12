using StorageMicroservice.Domain.DTOs;
using StorageMicroservice.Domain.Models;
using StorageMicroservice.Repository.IRepositories;
using StorageMicroservice.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Infrastructure.Services
{
    public class FileMetadataService : IFileMetadataService
    {
        private readonly IFileMetadataRepository metadataRepository;

        public FileMetadataService(IFileMetadataRepository metadataRepository)
        {
            this.metadataRepository = metadataRepository;
        }

        public async Task<FileMetadataCommandResponseDTO> AddFileMetadata(FileMetadata fileMetadata)
        {
            var response = new FileMetadataCommandResponseDTO();

            fileMetadata.Id = Guid.NewGuid();
            fileMetadata.UploadDate = DateTime.Now;

            response.result = await metadataRepository.CreateMetadataAsync(fileMetadata);

            response.Error = response.result ? string.Empty : "Error in adding metadata";

            return response;

        }

        public async Task<FileMetadataCommandResponseDTO> UpdateFileMetadata(FileMetadata fileMetadata)
        {
            var response = new FileMetadataCommandResponseDTO();

            var fileExist = await metadataRepository.HasMetadataAsync(fileMetadata.Id);

            if (!fileExist)
            {
                response.Error = "File Not exist..";
                response.result = false;
                return response;
            }

            response.result = await metadataRepository.UpdateMetadataAsync(fileMetadata);

            response.Error = response.result ? string.Empty : "Error in updating metadata";

            return response;
        }

        public async Task<FileMetadataCommandResponseDTO> DeleteFileMetadata(Guid FileId)
        {
            var responce = new FileMetadataCommandResponseDTO();
            var exist = await metadataRepository.HasMetadataAsync(FileId);
            if (!exist) 
            {
                responce.result = false;
                responce.Error = "File not exists.";
                return responce;
            }

            var file = await metadataRepository.GetMetadataAsync(FileId);

            responce.result = await metadataRepository.DeleteMetadataAsync(file);

            responce.Error = responce.result ? string.Empty : "Error in deleting metadata";

            return responce;
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFiles()
        {
            return await metadataRepository.GetAllMetadataAsync();
        }

        public async Task<FileMetadata> GetFileMetadata(Guid FileId)
        {
            return await metadataRepository.GetMetadataAsync(FileId);
        }



    }
}
