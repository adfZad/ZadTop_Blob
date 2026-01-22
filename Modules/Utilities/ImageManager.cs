using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using ZadHolding.Been;
using ZadHolding.PortalBase;
using System.Web;

namespace ZadHolding.Utilities
{
    public class ImageManager
    {
        public static bool GenerateThumbnails(Image image, FileType imageType, string fileName)
        {
            bool flag = true;
            try
            {
                string path = FileManager.GetSourceDirectory(imageType, fileName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Size newSize = ImageGenerator.CalculateDimensions(image.Size, new Size(0x272, 0x1ab));
                ImageGenerator.Resize(image, path + "L_" + fileName, newSize, ImageFormat.Jpeg);
                newSize = ImageGenerator.CalculateDimensions(image.Size, new Size(160, 120));
                ImageGenerator.Resize(image, path + "M_" + fileName, newSize, ImageFormat.Jpeg);
                newSize = ImageGenerator.CalculateDimensions(image.Size, new Size(0x37, 50));
                ImageGenerator.Resize(image, path + "S_" + fileName, newSize, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }

            return flag;
        }

        public static bool DeleteThumbnails(FileType fileType, string imageName)
        {
            bool flag = true;
            
            try
            {
                string path = FileManager.GetSourceDirectory(fileType, imageName);
                File.Delete(string.Format(@"{0}\L_{1}",path , imageName));
                File.Delete(string.Format(@"{0}\M_{1}", path, imageName));
                File.Delete(string.Format(@"{0}\S_{1}", path, imageName));

                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();
                if ((files.Length == 1) && (files[0].Name == "Thumbs.db"))
                {
                    File.Delete(string.Format(@"{0}\{1}", path, files[0].Name));
                    files = info.GetFiles();
                }

                if (files.Length == 0)
                {
                    Directory.Delete(path);
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }

            return flag;
        }

        public static string GenerateUniqueName(ImageFormat format)
        {
            return string.Format("{0}.{1}", Guid.NewGuid().ToString().Replace("-", ""), format.ToString());
        }

        

        public static bool GenerateUserThumbnails(Image image, string destFileName, string username)
        {
            bool flag = true;
            try
            {
                string path = "";// GetSourceDirectory(ImageSource.User, username, false);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Size newSize = ImageGenerator.CalculateDimensions(image.Size, new Size(0x237, 800));
                ImageGenerator.Resize(image, path + "L_" + destFileName, newSize, ImageFormat.Jpeg);
                newSize = ImageGenerator.CalculateDimensions(image.Size, new Size(170, 240));
                ImageGenerator.Resize(image, path + "M_" + destFileName, newSize, ImageFormat.Jpeg);
                newSize = ImageGenerator.CalculateDimensions(image.Size, new Size(0x55, 120));
                ImageGenerator.Resize(image, path + "S_" + destFileName, newSize, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }

            return flag;
        }

        public static Image GetImage(string imagePath)
        {
            Image image = null;

            try
            {
                image = Image.FromFile(imagePath);

            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Not a valid image file type", exception);
            }

            return image;
        }

        public static Image GetImage(System.Web.UI.WebControls.FileUpload file)
        {
            Image image = null;

            if (file.PostedFile == null)
            {
                throw new ArgumentException("No file to upload");
            }

            if (file.PostedFile.ContentLength == 0)
            {
                throw new ArgumentException("No data in selected file");
            }

            try
            {
                image = Image.FromStream(file.PostedFile.InputStream);
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Not a valid image file type", exception);
            }

            return image;
        }
    }
}
