using Microsoft.AspNetCore.Http;

namespace Application.Interface;

public interface IObjectStorage
{
    Task<string> UploadFile(IFormFile file, string subPath);
    Task<bool> DeleteFile(string objectName);
    Task<string> GetFileUrl(string objectName);
}