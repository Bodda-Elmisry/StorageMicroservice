using Microsoft.AspNetCore.Mvc;
using StorageMicroservice.DTOs;
using StorageMicroservice.Repository.Factories;
using StorageMicroservice.Repository.Providers;

namespace StorageMicroservice.Controllers
{
    [Route("FilesManager")]
    public class FileManagerController : Controller
    {
        private readonly IStorageProviderFactory providerFactory;
        private readonly IStorageProvider provider;

        public FileManagerController(IStorageProviderFactory providerFactory)
        {
            this.providerFactory = providerFactory;
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
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetFile")]
        public async Task<IActionResult> GetFile([FromBody] FileNameDTO fileName)
        {
            var result = await provider.GetFileAsync(fileName.FileName);
            return Ok(result);
        }

        [HttpPost("DeleteFile")]
        public async Task<IActionResult> DeleteFile([FromBody] FileNameDTO fileName)
        {
            try
            {
                await provider.DeleteFileAsync(fileName.FileName);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
