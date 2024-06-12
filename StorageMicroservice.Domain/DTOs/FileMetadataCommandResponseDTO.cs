using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Domain.DTOs
{
    public class FileMetadataCommandResponseDTO
    {
        public bool result { get; set; }
        public string Error { get; set; }
    }
}
