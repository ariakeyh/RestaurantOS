using Application.Interface;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace Infra.MinIo;

public class ObjectStorage(ILogger<ObjectStorage> logger, IMinioClient minIOClient, ICacheService cacheService, MinioConfig minioConfig) : IObjectStorage
{
    public async Task<string> UploadFile(IFormFile file, string? subpath = null)
    {
        try
        {
            logger.LogInformation("in minio service, opening file stream...");
            await using var fileStream = file.OpenReadStream();
            
            var extension = Path.GetExtension(file.FileName);
            var objectNameWithExtension = string.IsNullOrWhiteSpace(subpath)
                ? $"{file.Name}{Guid.NewGuid().ToString()[5..]}{extension}"
                : $"{subpath.TrimEnd('/')}/{file.Name}{Guid.NewGuid().ToString()[5..]}{extension}";
            
            logger.LogInformation("creating dto for minio...");
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(minioConfig.BucketName)
                .WithObject(objectNameWithExtension)
                .WithStreamData(fileStream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);

            logger.LogInformation("uploading...");

            var obj = await minIOClient.PutObjectAsync(putObjectArgs);
            
            logger.LogInformation("minio response status code : {obj.ResponseStatus}",obj.ResponseStatusCode);
            logger.LogInformation("minio response content : {ResponseContent}", obj.ResponseContent);
            
            if(string.IsNullOrWhiteSpace(obj.Etag))
                logger.LogError("ETag is null - {ResponseStatusCode} - {ResponseContent}",obj.ResponseStatusCode,obj.ResponseContent);

            return objectNameWithExtension;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string> GetFileUrl(string objectName)
    {
        if (string.IsNullOrWhiteSpace(objectName))
            return string.Empty;

        var url = await cacheService.GetValue<string>("minio:" + objectName);

        if (!string.IsNullOrWhiteSpace(url))
            return url;
        
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(minioConfig.BucketName)
                .WithObject(objectName)
                .WithExpiry(86400);

            url = await minIOClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            logger.LogInformation($"Presigned URL for '{objectName}': {url}");
            await cacheService.SetValue("minio:" + objectName, url, TimeSpan.FromSeconds(86400));
            return url;
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Error generating presigned URL: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteFile(string objectName)
    {
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(minioConfig.BucketName)
                .WithObject(objectName);

            await minIOClient.RemoveObjectAsync(args);

            logger.LogInformation("Deleted object {ObjectName} from bucket {BucketName}", objectName, minioConfig.BucketName);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete object {ObjectName} from bucket {BucketName}", objectName, minioConfig.BucketName);
            return false;
        }
    }
}