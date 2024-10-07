using ECommerceSolution.Service.Enums;

namespace ECommerceSolution.Infrastructure.FileService;
  public class FileStorageSettings
  {
      public Dictionary<StorageType, string> Paths { get; set; } = new();
  }
