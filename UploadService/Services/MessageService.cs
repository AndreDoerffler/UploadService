using UploadService.Models;

namespace UploadService.Services;

public class MessageService : IMessageService  
{
    public bool SendMessage(CustomerModel customer, string s)
    {
        Console.WriteLine($"Message:\n {s}\nsent to customerId {customer.CustomerID}");
        return true;
    }
}