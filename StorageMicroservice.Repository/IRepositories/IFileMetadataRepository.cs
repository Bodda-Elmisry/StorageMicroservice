using StorageMicroservice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Repository.IRepositories
{
    public interface IFileMetadataRepository
    {
        Task<bool> CreateMetadataAsync(FileMetadata fileMetadata);

        Task<bool> UpdateMetadataAsync(FileMetadata fileMetadata);

        Task<bool> DeleteMetadataAsync(FileMetadata fileMetadata);

        Task<IEnumerable<FileMetadata>> GetAllMetadataAsync();

        Task<FileMetadata> GetMetadataAsync(Guid metadataId);
        Task<FileMetadata?> GetMetadataByNameAsync(string fileName);

        Task<bool> HasMetadataAsync(Guid metadataId);

    }
}
