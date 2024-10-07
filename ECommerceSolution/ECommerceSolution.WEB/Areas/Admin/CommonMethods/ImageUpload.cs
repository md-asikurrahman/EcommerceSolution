namespace ECommerceSolution.WEB.Areas.Admin.CommonMethods
{
    public class ImageUpload
    {
        private readonly IWebHostEnvironment _host;
        public ImageUpload(IWebHostEnvironment host)
        {
           _host = host; 
        }
        public async Task<string> UploadFileAsync(IFormFile file, string folderPath, string prefix )
        {
            // Check if the file is null or empty
            if (file == null || file.Length == 0)
            {
                return "NoImage.jpg"; // Return a default image name when no file is uploaded
            }

            // Get the file extension
            string fileExt = Path.GetExtension(file.FileName);

            // Generate a unique file name using the prefix and timestamp
            string fileName = $"{prefix}{DateTime.Now.ToString("ssmmhhddMMyyyy")}{fileExt}";

            // Get the root directory (wwwroot)
            string rootPath = _host.WebRootPath;

            // Combine the root path and folder path to get the full path for saving
            string fullFolderPath = Path.Combine(rootPath, folderPath);

            // Ensure the directory exists
            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }

            // Combine the folder path and file name to get the full file path
            string fullPath = Path.Combine(fullFolderPath, fileName);

            // Save the file asynchronously
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the file name to be used (e.g., saving in the database)
            return fileName;
        }

    }
}
