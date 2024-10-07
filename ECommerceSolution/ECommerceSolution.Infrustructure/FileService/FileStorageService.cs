using ECommerceSolution.Service.Enums;
using ECommerceSolution.Service.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace ECommerceSolution.Infrastructure.FileService
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;
        private readonly FileStorageSettings _fileStorageSettings;
        private readonly string _baseUrl;
        public FileStorageService(IOptions<FileStorageSettings> fileStorageSettingsOptions,
        IHttpContextAccessor httpContextAccessor,
        string webRootPath)
        {
            _webRootPath = webRootPath;
            _fileStorageSettings = fileStorageSettingsOptions.Value;
            _baseUrl =
                $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext!.Request.Host.Value}{httpContextAccessor.HttpContext!.Request.PathBase.Value}";
        }
        public Task<string> SaveAsync(string base64Data, StorageType storageType)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(string fileName, StorageType storageType)
        {
            throw new NotImplementedException();
        }

        public string FileUrl(string? fileName, StorageType storageType)
        {
            throw new NotImplementedException();
        }

        public Task<string> FileUrlAsync(string fileName, StorageType storageType)
        {
            throw new NotImplementedException();
        }

        
    }
}
