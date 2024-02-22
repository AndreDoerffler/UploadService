using Microsoft.AspNetCore.Http;
using UploadService.Models;
using UploadService.Services;

namespace UnitTests;

public class UploadServiceTests
{
    private IFileUploadService _fileUploadService;
    
    [SetUp]
    public void Setup()
    {
        IMessageService messageService = new MessageService();
        _fileUploadService = new FileUploadService(messageService);
    }

    [Test]
    public void UploadCustomerFiles_Works()
    {
        var request = new UploadFilesModel()
        {
            UserId = 1,
            CustomerId = 5,
            Files = new List<IFormFile>() { }
        };
        
        var response = _fileUploadService.UploadCustomerFiles(request);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.IsNotEmpty(response.TrackingId.ToString());
    }

    [Test]
    public void CheckStatus_Works()
    {
        var request = new UploadFilesModel()
        {
            UserId = 1,
            CustomerId = 5,
            Files = new List<IFormFile>() { }
        };
        
        var uploadResponse = _fileUploadService.UploadCustomerFiles(request);
        
        var response = _fileUploadService.CheckTrackingStatus(uploadResponse.TrackingId);

        Assert.NotNull(response);
    }
}