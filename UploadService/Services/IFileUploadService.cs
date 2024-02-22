using UploadService.Models;

namespace UploadService.Services;

public interface IFileUploadService
{
    ResponseModel UploadCustomerFiles(UploadFilesModel request);
    TrackingModel CheckTrackingStatus(Guid trackingId);
}