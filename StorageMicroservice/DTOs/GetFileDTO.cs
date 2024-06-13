using Microsoft.AspNetCore.Mvc;
using StorageMicroservice.Domain.Models;

namespace StorageMicroservice.DTOs
{
    public class GetFileDTO
    {
        public FileMetadata FileMetadata { get; set; }
        public Stream Stream { get; set; }
    }
}
