using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.Services.Attachment
{
    public class AttachmentServices : IAttachmentServices
    {
        List<string> AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png"};
        const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
        public string? Upload(IFormFile file, string FolderName)
        {
            // 1. check the extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if(!AllowedExtensions.Contains(extension)) return null;

            // 2. check the file size
            if (file.Length > MaxFileSize || file.Length ==0 ) return null;

            // 3. get located folder path
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files" , FolderName);

            // 4. Make attacment file is unique
            var FileName = $"{Guid.NewGuid()}_{extension}";

            // 5. Get the full path
            var fullPath = Path.Combine(FolderPath, FileName);

            // 6. create file stream to copy file [unManaged]
            using FileStream fileStream = new FileStream(fullPath, FileMode.Create);   // create the file if not exists and overwrite it if exists

            //7. use stream copy to save the file
            file.CopyTo(fileStream);

            // 8. return the file name to store it in the database
            return FileName;

        }

        public bool Delete(string? fileName , string? folderName)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName))
            {
                return false; // Invalid input
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception)
                {
                    // Log the exception if needed
                    return false;
                }
            }

            return false; // File does not exist

        }

    }
}
