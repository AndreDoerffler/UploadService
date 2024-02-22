namespace UploadService.Models;

public class UploadFilesModel : UserCustomerModel
{
    public List<IFormFile> Files{ get; set; }
}