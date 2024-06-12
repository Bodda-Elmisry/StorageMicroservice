
using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Infrastructure.Data;
using StorageMicroservice.Infrastructure.Services;
using StorageMicroservice.Repository.Configrations;
using StorageMicroservice.Repository.Factories;
using StorageMicroservice.Repository.IRepositories;
using StorageMicroservice.Repository.IServices;
using StorageMicroservice.Repository.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options =>
        options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ),
                ServiceLifetime.Singleton
        );

builder.Services.Configure<AppConfigrations>(builder.Configuration.GetSection("AppConfigrations"));

builder.Services.AddScoped<IFileMetadataRepository, IFileMetadataRepository>();

builder.Services.AddScoped<IFileMetadataService, FileMetadataService>();

builder.Services.AddSingleton<LocalStorageProvider>();
builder.Services.AddSingleton<AzureStorageProvider>();

builder.Services.AddSingleton<IStorageProviderFactory, StorageProviderFactory>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
