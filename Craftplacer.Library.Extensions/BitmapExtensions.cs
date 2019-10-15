using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Craftplacer.Library.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap Crop(this Bitmap bitmap, int x, int y, int width, int height) => bitmap.Crop(new Rectangle(x, y, width, height));

        public static Bitmap Crop(this Bitmap bitmap, Rectangle rectangle)
        {
            var bmpImage = new Bitmap(bitmap);
            return bmpImage.Clone(rectangle, bmpImage.PixelFormat);
        }

        /// <summary>
        /// Clips the <paramref name="sourceImage"/> to the <paramref name="destinationSize"/>
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="destinationSize"></param>
        /// <returns>The clipped image</returns>
        public static Bitmap ClipToCircle(this Bitmap sourceImage, Size destinationSize)
        {
            Bitmap dstImage = new Bitmap(destinationSize.Width * 2, destinationSize.Height * 2, PixelFormat.Format32bppArgb);
            var center = new PointF(destinationSize.Width, destinationSize.Height);
            int radius = destinationSize.Width;
            using (var graphics = Graphics.FromImage(dstImage))
            {
                graphics.SetQuality();
                var r = new RectangleF(center.X - radius, center.Y - radius,
                                              radius * 2, radius * 2);
                var path = new GraphicsPath();
                path.AddEllipse(r);
                graphics.SetClip(path);
                graphics.DrawImage(sourceImage, new Rectangle(Point.Empty, new Size(destinationSize.Width * 2, destinationSize.Height * 2)));
                return ResizeImage(dstImage, destinationSize.Width, destinationSize.Height);
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="bitmap">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(this Bitmap bitmap, Size size) => ResizeImage(bitmap, size.Width, size.Height);

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="bitmap">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(this Bitmap bitmap, int width, int height)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            //Check if a resize is needed.
            if (bitmap.Size.Equals(new Size(width, height)))
            {
                return (Bitmap)bitmap;
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            destImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.SetQuality();
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
    }
}
