using ECommerceSolution.Service.Enums;

namespace ECommerceSolution.Service.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(string base64Data, StorageType storageType);

        Task DeleteAsync(string fileName, StorageType storageType);

        Task<string> FileUrlAsync(string fileName, StorageType storageType);

        string FileUrl(string? fileName, StorageType storageType);
    }
}
