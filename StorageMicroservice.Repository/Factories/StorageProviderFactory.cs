﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StorageMicroservice.Repository.Configrations;
using StorageMicroservice.Repository.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Repository.Factories
{
    public class StorageProviderFactory : IStorageProviderFactory
    {
        private readonly AppConfigrations appConfigrations;
        private readonly IServiceProvider serviceProvider;
        private readonly ILocalStorageProvider localStorageProvider;

        public StorageProviderFactory(IOptions<AppConfigrations> appConfigrations, IServiceProvider serviceProvider)
        {
            this.appConfigrations = appConfigrations.Value;
            this.serviceProvider = serviceProvider;
            //this.localStorageProvider = localStorageProvider;
        }

        public IStorageProvider GetProvider()
        {
            IStorageProvider provider;

            switch(appConfigrations.StorageType.ToLower())
            {
                case "local":
                    provider = serviceProvider.GetRequiredService<LocalStorageProvider>();
                    break;
                case "azure":
                    provider = serviceProvider.GetRequiredService<AzureStorageProvider>();
                    break;
                default:
                    throw new ArgumentException("Invaled storage type");

            }

            return provider;
        }
    }
}
