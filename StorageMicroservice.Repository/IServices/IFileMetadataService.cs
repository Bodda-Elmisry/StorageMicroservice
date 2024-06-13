using StorageMicroservice.Domain.DTOs;
using StorageMicroservice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Repository.IServices
{
    public interface IFileMetadataService
    {
        Task<FileMetadataCommandResponseDTO> AddFileMetadata(FileMetadata fileMetadata);
        Task<FileMetadataCommandResponseDTO> DeleteFileMetadata(FileMetadata FileMetadata);
        Task<FileMetadataCommandResponseDTO> UpdateFileMetadata(FileMetadata fileMetadata);
        Task<IEnumerable<FileMetadata>> GetAllFiles();
        Task<FileMetadata> GetFileMetadata(Guid FileId);
        Task<FileMetadata> GetFileMetadataByName(string fileName);

    }
}
