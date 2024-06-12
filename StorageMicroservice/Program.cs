
using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Infrastructure.Data;
using StorageMicroservice.Infrastructure.Repository;
using StorageMicroservice.Infrastructure.Services;
using StorageMicroservice.Repository.Configrations;
using StorageMicroservice.Repository.Factories;
using StorageMicroservice.Repository.IRepositories;
using StorageMicroservice.Repository.IServices;
using StorageMicroservice.Repository.Providers;
using Microsoft.Extensions.Azure;

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

builder.Services.AddSingleton<LocalStorageProvider>();
builder.Services.AddSingleton<AzureStorageProvider>();

builder.Services.AddSingleton<IStorageProviderFactory, StorageProviderFactory>();

builder.Services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();

builder.Services.AddScoped<IFileMetadataService, FileMetadataService>();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["localstorage:blob"]!, preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["localstorage:queue"]!, preferMsi: true);
});





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
