using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using System.Drawing.Imaging;
using System.Web;
using ZadHolding.PortalBase;
using System.IO;
using System.Configuration;

namespace ZadHolding.Utilities
{
    public class FileManager
    {
        private static IFileStorage _storage;

        static FileManager()
        {
            InitializeStorage();
        }

        private static void InitializeStorage()
        {
            string provider = System.Configuration.ConfigurationManager.AppSettings["StorageProvider"];
            if (string.Equals(provider, "AzureBlob", StringComparison.OrdinalIgnoreCase))
            {
                _storage = new AzureBlobStorage();
            }
            else
            {
                _storage = new LocalFileStorage();
            }
        }

        public static string GenerateUniqueName(FileType imageType)
        {
            string fileName = string.Empty;

            if (imageType == FileType.Photo)
            {
                fileName = string.Format("{0}.{1}", Guid.NewGuid().ToString().Replace("-", ""), ImageFormat.Jpeg.ToString());
            }
            else
            {
                fileName = string.Format("{0}.pdf", Guid.NewGuid().ToString().Replace("-", ""));
            }

            return fileName;
        }

        public static string GetSourceDirectory(FileType fileType, string fileName)
        {
            // This logic is specific to how directory structure was built.
            // Old logic returns full path: rootDirectory + fileType + fileName...
            // New logic needs to return relative path for URL or directory descriptor for storage.
            
            // For backward compatibility or internal usage, we should check if we need the URL or the abstract directory.
            // But this method was public static string.
            
           return _storage.GetFileUrl(string.Format(@"{0}\{1}\{2}\", fileType, fileName.Substring(0, 1), fileName.Substring(0, 2)), fileName);
        }
        
        // Helper to get the directory part for storage operations, not the URL
        private static string GetStorageDirectory(FileType fileType, string fileName)
        {
             return string.Format(@"{0}\{1}\{2}\", fileType, fileName.Substring(0, 1), fileName.Substring(0, 2));
        }

        public static bool SaveFile(System.Web.UI.WebControls.FileUpload file, FileType fileType, string fileName)
        {
            bool result = true;

            try
            {
                //if (fileType == FileType.Photo)
                //{
                //    result = ImageManager.GenerateThumbnails(ImageManager.GetImage(file), fileType, fileName);
                //}
                //else
                //{
                    string directory = GetStorageDirectory(fileType, fileName);
                    _storage.SaveFile(file.PostedFile, directory, fileName);
                    
                //}
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        public static void SaveFile(Stream stream, FileType fileType, string fileName)
        {
             string directory = GetStorageDirectory(fileType, fileName);
             _storage.SaveFile(stream, directory, fileName);
        }

        public static Stream GetFileStream(FileType fileType, string fileName)
        {
            string directory = GetStorageDirectory(fileType, fileName);
            return _storage.GetFileStream(directory, fileName);
        }

        public static bool FileExists(FileType fileType, string fileName)
        {
            string directory = GetStorageDirectory(fileType, fileName);
            return _storage.FileExists(directory, fileName);
        }
    }
}
