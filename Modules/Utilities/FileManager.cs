using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using System.Drawing.Imaging;
using System.Web;
using ZadHolding.PortalBase;
using System.IO;

namespace ZadHolding.Utilities
{
    public class FileManager
    {
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
            string rootDirectory = HttpContext.Current.Server.MapPath(@"~\" + WebConfigKeys.UploadImageRootDirectory);
            return string.Format(@"{0}\{1}\{2}\{3}\", rootDirectory, fileType, fileName.Substring(0, 1), fileName.Substring(0, 2));
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

                    string filePath = GetSourceDirectory(fileType, fileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    file.SaveAs(string.Format("{0}{1}", filePath, fileName));
                //}
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }
    }
}
