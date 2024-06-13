using Microsoft.AspNetCore.Mvc;
using StorageMicroservice.Domain.Models;
using StorageMicroservice.DTOs;
using StorageMicroservice.Repository.Factories;
using StorageMicroservice.Repository.IServices;
using StorageMicroservice.Repository.Providers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StorageMicroservice.Controllers
{
    [Route("FilesManager")]
    public class FileManagerController : Controller
    {
        private readonly IStorageProviderFactory providerFactory;
        private readonly IFileMetadataService metadataService;
        private readonly IStorageProvider provider;

        public FileManagerController(IStorageProviderFactory providerFactory,
                                     IFileMetadataService metadataService)
        {
            this.providerFactory = providerFactory;
            this.metadataService = metadataService;
            provider = providerFactory.GetProvider();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> AddFile(AddFileDTO fileDTO)
        {
            try
            {
                var filename = fileDTO.File.FileName;
                var newFileName = DateTime.Now.ToString("mmss") + "_" + filename;
                await provider.SaveFileAsync(newFileName, fileDTO.File);
                var metadata = new FileMetadata
                {
                    FileName = newFileName,
                    Size = fileDTO.File.Length,
                    ContentType = fileDTO.File.ContentType
                };
                var added = await metadataService.AddFileMetadata(metadata);
                if (added != null && added.result)
                {
                    return Ok(metadata);

                }
                else
                {
                    return BadRequest(added != null ? added.Error: string.Empty);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetFile")]
        public async Task<IActionResult> GetFile([FromBody] FileNameDTO fileName)
        {
            var stream = await provider.GetFileAsync(fileName.FileName);
            var metadata = await metadataService.GetFileMetadataByName(fileName.FileName);

            var result = new GetFileDTO
            {
                FileMetadata = metadata,
                Stream = stream
            };
            return Ok(result);
        }

        [HttpPost("DeleteFile")]
        public async Task<IActionResult> DeleteFile([FromBody] FileNameDTO fileName)
        {
            try
            {

                await provider.DeleteFileAsync(fileName.FileName);
                var metadata = await metadataService.GetFileMetadataByName(fileName.FileName);
                var deleted = await metadataService.DeleteFileMetadata(metadata);
                return (deleted != null && deleted.result) ? Ok() : BadRequest(deleted != null ? deleted.Error : "Error in deleting");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
