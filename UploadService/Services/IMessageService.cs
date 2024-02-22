using UploadService.Models;

namespace UploadService.Services;

public interface IMessageService
{
    bool SendMessage(CustomerModel customer, string s);
}