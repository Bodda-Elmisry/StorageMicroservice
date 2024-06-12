using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Domain.Models;
using StorageMicroservice.Infrastructure.Data;
using StorageMicroservice.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Infrastructure.Repository
{
    public class FileMetadataRepository : IFileMetadataRepository
    {
        private readonly AppDBContext context;

        public FileMetadataRepository(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateMetadataAsync(FileMetadata fileMetadata)
        {
            context.FilesMetadata.Add(fileMetadata);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMetadataAsync(FileMetadata fileMetadata)
        {
            context.FilesMetadata.Update(fileMetadata);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMetadataAsync(FileMetadata fileMetadata)
        {
            context.FilesMetadata.Remove(fileMetadata);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FileMetadata>> GetAllMetadataAsync()
        {
            return await context.FilesMetadata.ToListAsync();
        }

        public async Task<FileMetadata> GetMetadataAsync(Guid metadataId)
        {
            return await context.FilesMetadata.FirstOrDefaultAsync(e => e.Id == metadataId);
        }

        public async Task<bool> HasMetadataAsync(Guid metadataId)
        {
            return await context.FilesMetadata.AnyAsync(f=> f.Id == metadataId);
        }
    }
}
