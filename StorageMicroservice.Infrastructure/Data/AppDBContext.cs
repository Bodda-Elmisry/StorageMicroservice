using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMicroservice.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options)
            :base(options) 
        {
            
        }


        public DbSet<FileMetadata> FilesMetadata { get; set; }



    }
}
