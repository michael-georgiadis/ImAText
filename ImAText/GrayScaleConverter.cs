using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace ImAText
{
    public static class GrayScaleConverter
    {
        public static bool IsGrayScale(this Bitmap image)
        {
            for (var i = 0; i < image.Height; i++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    var pixel = image.GetPixel(i, j);

                    var r = pixel.R;
                    var g = pixel.G;
                    var b = pixel.B;

                    if (r != g && b != g)
                        return false;
                }
            }

            return true;
        }

        public static Bitmap ConvertToGrayScale(this Bitmap image)
        {
            int destWidth, destHeight;

            if (image.Width > image.Height)
            {
                destWidth = image.Width > 900 ? image.Width / 10 : image.Width / 5;
                destHeight = image.Height > 700 ? image.Height / 10 : image.Height / 7;
            } else if (image.Height > image.Width)
            {
                destHeight = image.Height > 900 ? image.Height / 10 : image.Height / 5;
                destWidth = image.Width > 700 ? image.Width / 10 : image.Width / 7;
            } else
            {
                destHeight = image.Height > 700 ? image.Height / 10 : image.Height / 5;
                destWidth = image.Width > 700 ? image.Width / 10 : image.Width / 5;
            }


            var grayImage = new Bitmap(destWidth, destHeight);

            using (var graphics = Graphics.FromImage(grayImage))
            {
                var colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                        new float[] { .3f, .3f, .3f, 0, 0},
                        new float[] { .59f, .59f, .59f, 0, 0 },
                        new float[] { .11f, .11f, .11f, 0, 0},
                        new float[] { 0, 0, 0, 1, 0 },
                        new float[] { 0, 0, 0, 0, 1 }
                    });

                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    attributes.SetWrapMode(WrapMode.TileFlipXY);

                    graphics.DrawImage(image, new Rectangle(0, 0, destWidth, destHeight),
                        0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return grayImage;
        }

        public static Bitmap Resize(this Bitmap image, int width, int height)
        {
            var destRec = new Rectangle(0, 0, width, height);
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
                    graphics.DrawImage(image, destRec, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
