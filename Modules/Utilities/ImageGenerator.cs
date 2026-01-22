using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace ZadHolding.Utilities
{
    public class ImageGenerator
    {
        // Methods
        public static Size CalculateDimensions(Size imgSize, Size reSizeTo)
        {
            Size size = new Size();
            double width = imgSize.Width;
            double height = imgSize.Height;
            double num3 = imgSize.Width;
            double num4 = imgSize.Height;
            double num5 = reSizeTo.Width;
            double num6 = reSizeTo.Height;
            double num7 = num5 / width;
            double num8 = num6 / height;
            if ((width <= num5) && (height <= num6))
            {
                return new Size((int)num3, (int)num4);
            }
            if ((num7 * height) < num6)
            {
                num4 = Math.Ceiling((double)(num7 * height));
                num3 = num5;
            }
            else
            {
                num3 = Math.Ceiling((double)(num8 * width));
                num4 = num6;
            }
            size.Width = (int)num3;
            size.Height = (int)num4;
            return size;
        }

        public static bool Resize(Image srcImage, string dstFilePath, Size newSize, ImageFormat format)
        {
            using (Bitmap bitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppPArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(new Point(0, 0), newSize));
                    bitmap.Save(dstFilePath, format);
                }
            }

            return true;
        }

        public static bool Resize(string srcFilePath, string dstFilePath, Size newSize, ImageFormat format)
        {
            if (!File.Exists(srcFilePath))
            {
                throw new FileNotFoundException("Unable to find " + srcFilePath + " to resize.\n");
            }
            using (Image image = Image.FromFile(srcFilePath))
            {
                return Resize(image, dstFilePath, newSize, format);
            }
        }
    }
}
