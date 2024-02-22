namespace UploadService.Models;

public class ResponseModel
{
    public Guid TrackingId { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}