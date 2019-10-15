using System;

namespace Craftplacer.Library.Extensions
{
    public static class IntegerExtensions
    {
        public static System.Drawing.Color ToRgbColor(this int rgbColor)
        {
            return System.Drawing.Color.FromArgb(rgbColor);
        }

        public static System.Drawing.Color ToBgrColor(this int bgrColor)
        {
            byte[] bytes = BitConverter.GetBytes(bgrColor);
            return System.Drawing.Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        }
    }
}
