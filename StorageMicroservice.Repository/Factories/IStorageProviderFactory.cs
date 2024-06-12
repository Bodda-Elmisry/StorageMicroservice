using StorageMicroservice.Repository.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Repository.Factories
{
    public interface IStorageProviderFactory
    {
        IStorageProvider GetProvider();
    }
}
