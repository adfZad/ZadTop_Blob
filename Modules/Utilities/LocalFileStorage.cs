using System;
using System.IO;
using System.Web;
using ZadHolding.PortalBase;

namespace ZadHolding.Utilities
{
    public class LocalFileStorage : IFileStorage
    {
        public string SaveFile(HttpPostedFile file, string directory, string fileName, System.Collections.Generic.Dictionary<string, string> metadata = null)
        {
            string rootDirectory = HttpContext.Current.Server.MapPath(@"~\" + WebConfigKeys.UploadImageRootDirectory);
            string filePath = Path.Combine(rootDirectory, directory);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fullPath = Path.Combine(filePath, fileName);
            file.SaveAs(fullPath);

            return fullPath;
        }

        public void SaveFile(Stream stream, string directory, string fileName, System.Collections.Generic.Dictionary<string, string> metadata = null)
        {
            string rootDirectory = HttpContext.Current.Server.MapPath(@"~\" + WebConfigKeys.UploadImageRootDirectory);
            string filePath = Path.Combine(rootDirectory, directory);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fullPath = Path.Combine(filePath, fileName);
            using (var fileStream = File.Create(fullPath))
            {
                stream.CopyTo(fileStream);
            }
        }

        public string GetFileUrl(string directory, string fileName)
        {
             // This needs to match the structure expected by the frontend: "../../Uploads/Passport/1/2d/1234.pdf"
             // directory usually passed in is like "Passport\1\2d\"
             
             // Constructing relative path.
             // directory input from FileManager is: fileType \ fileName(0,1) \ fileName(0,2) \
             
             string relativePath = string.Format("../../{0}/{1}{2}", 
                 WebConfigKeys.UploadImageRootDirectory, 
                 directory.Replace("\\", "/"), 
                 fileName);
                 
             return relativePath;
        }

        public bool FileExists(string directory, string fileName)
        {
            string rootDirectory = HttpContext.Current.Server.MapPath(@"~\" + WebConfigKeys.UploadImageRootDirectory);
            string fullPath = Path.Combine(rootDirectory, directory, fileName);
            return File.Exists(fullPath);
        }

        public Stream GetFileStream(string directory, string fileName)
        {
            string rootDirectory = HttpContext.Current.Server.MapPath(@"~\" + WebConfigKeys.UploadImageRootDirectory);
            string fullPath = Path.Combine(rootDirectory, directory, fileName);
            
            if (File.Exists(fullPath))
            {
                return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            }
            return null;
        }
    }
}
