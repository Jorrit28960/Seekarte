using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Seekarte
{
    class Functions
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void ResizeImage()
        {
            if (Program.formStartPage != null)
            {
                double width = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44.Width;
                double height = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44.Height;
                double ratio = height / width;

                //double ratioWidth = Program.formStartPage.Map.Width / width;
                double ratioWidth = Program.formStartPage.GetMapWidth() / width;
                double ratioHeight = Program.formStartPage.Map.Height / height;

                if (ratioWidth > ratioHeight)
                {
                    Program.formStartPage.Map.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, (int)Math.Round(Program.formStartPage.Map.Height / ratio), Program.formStartPage.Map.Height);
                }
                else
                {
                    Program.formStartPage.Map.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, Program.formStartPage.Map.Width, (int)Math.Round(Program.formStartPage.Map.Width * ratio));
                }
            }
        }
    }
}
