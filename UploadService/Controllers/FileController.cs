using Microsoft.AspNetCore.Mvc;
using UploadService.Models;
using UploadService.Services;

namespace UploadService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FileController : Controller
{
    private readonly IFileUploadService _fileUploadService;

    public FileController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
    public ResponseModel UploadFiles(UploadFilesModel request)
    {
        var result = _fileUploadService.UploadCustomerFiles(request);
        
        return result;
    }

    
    [HttpGet]
    public ActionResult CheckStatus(Guid trackingId)
    {
        try
        {
            var result = _fileUploadService.CheckTrackingStatus(trackingId);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex);
            // TrackingId may be wrong
            return NotFound();
        }
    }
}