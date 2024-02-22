using UploadService.Models;

namespace UploadService.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IMessageService _messageService;
    
    // Mockdata, could be moved to seperate file/class; maybe use a factory here
    private readonly Dictionary<int, UserModel> _userStore = new() 
    {
        { 1, new UserModel() 
            { 
                UserID = 1, 
                CustomerList = new List<CustomerModel>()
                {
                    new CustomerModel() { CustomerID = 5, FileList = new List<IFormFile>() },
                    new CustomerModel() { CustomerID = 13, FileList = new List<IFormFile>() },
                }
            }
        }
    };

    private readonly Dictionary<Guid, TrackingModel> _tracking = new();

    
    public FileUploadService(IMessageService messageService)
    {
        _messageService = messageService;
    }

    CustomerModel GetCustomer(int userId, int customerId)
    {
        var customer = _userStore[userId].CustomerList.FirstOrDefault(x => x.CustomerID == customerId);
        
        if (customer == null)
            throw new Exception($"Customer with ID {customerId} not found.");

        return customer;
    }

    private async Task StoreFiles(Guid trackingId, CustomerModel customer)
    {
        await Task.Delay(TimeSpan.FromSeconds(30));

        _tracking[trackingId].IsCompleted = true;

        _messageService.SendMessage(customer, $"The upload for TrackingID {trackingId} is completed.");
    }
    
    public ResponseModel UploadCustomerFiles(UploadFilesModel request)
    {
        var trackingId = Guid.NewGuid();
        var customer = GetCustomer(request.UserId, request.CustomerId); 
        
        customer.FileList.AddRange(request.Files);
        _tracking.Add(trackingId, new TrackingModel()
        {
            UserId = request.UserId,
            CustomerId = request.CustomerId,
            StartTime = DateTime.Now,
            IsCompleted = false
        });
        
        // Upload files and do awesome stuff
        StoreFiles(trackingId, customer);
        
        return new ResponseModel()
        {
            TrackingId = trackingId,
            Message = $"{request.Files.Count} files uploading for CustomerId {request.CustomerId}",
            IsSuccess = true,
        };
    }

    public TrackingModel CheckTrackingStatus(Guid trackingId)
    {
        return _tracking[trackingId];
    }
}