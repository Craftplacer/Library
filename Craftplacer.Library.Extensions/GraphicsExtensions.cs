using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Craftplacer.Library.Extensions
{
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Sets all properties of a <see cref="Graphics"/> object to the corresponding <paramref name="quality"/> parameter.
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics"/> to be changed</param>
        /// <param name="quality">The <see cref="GraphicsQuality"/> that should it be set to</param>
        public static void SetQuality(this Graphics graphics, GraphicsQuality quality = GraphicsQuality.High)
        {
            switch (quality)
            {
                case GraphicsQuality.High:
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    break;
                case GraphicsQuality.Normal:
                    graphics.SmoothingMode = SmoothingMode.Default;
                    graphics.CompositingQuality = CompositingQuality.Default;
                    graphics.InterpolationMode = InterpolationMode.Default;
                    graphics.PixelOffsetMode = PixelOffsetMode.Default;
                    graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
                    break;
                case GraphicsQuality.Low:
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    break;
            }
            
        }


    }

    /// <summary>
    /// Defines quality levels for use in <seealso cref="GraphicsExtensions.SetQuality"/>
    /// </summary>
    public enum GraphicsQuality
    {
        High,
        Normal,
        Low
    }
}