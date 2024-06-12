using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Repository.Configrations
{
    public class AppConfigrations
    {
        public string LocalStoragePath { get; set; }
        public string AzureBlobStorageConnectionString { get; set; }
        public string AzureBlobStorageContainerName { get; set; }
    }
}
