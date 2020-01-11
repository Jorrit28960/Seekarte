using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Seekarte
{
    class FunctionLayouts
    {
        private static Country polen = new Country("Polen");
        private static Country tartarenreich = new Country("Tartarenreich");

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
                int widthMap = Program.formStartPage.GetMapWidth();
                int heightMap = Program.formStartPage.GetMapHeight();
                double ratio = height / width;

                //double ratioWidth = Program.formStartPage.Map.Width / width;
                double ratioWidth = widthMap / width;
                double ratioHeight = heightMap / height;

                if (ratioWidth > ratioHeight)
                {
                    Program.formStartPage.SetMapImage(FunctionLayouts.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, (int)Math.Round(heightMap / ratio), heightMap));
                }
                else
                {
                    Program.formStartPage.SetMapImage(FunctionLayouts.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, widthMap, (int)Math.Round(widthMap * ratio)));
                }
            }
        }

        private static Country GetCountry(string buttonAcssibleName)
        {
            switch (buttonAcssibleName)
            {
                case "Polen":
                    return polen;
                case "Tartarenreich":
                    return tartarenreich;
            }

            return new Country("test");
        }

        private static bool Password(Country country)
        {
            string pwdInput;
            if (!country.passwordSet)
            {
                var pwdOutput = Interaction.InputBox("Bitte legen Sie ihr Passwort fest", "Passwort", "");
                country.SetPassowrd(pwdOutput);
            }

            pwdInput = Interaction.InputBox("Bitte geben Sie ihr Passwort ein", "Passwort", "");
            country.passwordSet = true;

            if (pwdInput == country.GetPassword())
            {
                return true;
            }
            else
            {
                MessageBox.Show("Falsches Passwort", "Falsches Passwort", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void ActivateCountry(Button sender, EventArgs e)
        {
            Country country = GetCountry(sender.Text);
            //MessageBox.Show(sender.Text, "Enter Password", MessageBoxButtons.OK, MessageBoxIcon.Information);



            if (Password(country))
            {
                //this.Hide();
                //Program.formStartPage.Hide();
                //FormCountry formCoutry = new FormCountry();
                //formCoutry.Show();

                Program.formStartPage.SetCountryLabel(country.GetName());
                Program.formStartPage.SetCountryLabel(true);
            }

        }
    }
}
