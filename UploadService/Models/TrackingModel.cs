namespace UploadService.Models;

public class TrackingModel : UserCustomerModel
{
    public DateTime StartTime { get; set; }
    public bool IsCompleted { get; set; }
}