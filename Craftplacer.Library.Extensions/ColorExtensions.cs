using System;
using System.Drawing;

namespace Craftplacer.Library.Extensions
{
    public static class ColorExtensions
    {
        public static int ToBgrInt(this Color color) => (0 << 24) + (color.B << 16) + (color.G << 8) + color.R;

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1.
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeBrightness(this Color color, float correctionFactor)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = ((255 - red) * correctionFactor) + red;
                green = ((255 - green) * correctionFactor) + green;
                blue = ((255 - blue) * correctionFactor) + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        //https://www.codeproject.com/Articles/16565/Determining-Ideal-Text-Color-Based-on-Specified-Ba
        /// <summary>
        /// Returns a readable color generated from the input <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The background color</param>
        /// <returns>A readable color</returns>
        public static Color GetReadableColor(this Color color)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114));
            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        /// <summary>Blends the specified colors together.</summary>
        /// <param name="color">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amount">How much of <paramref name="color"/> to keep,
        /// “on top of” <paramref name="backColor"/>.</param>
        /// <returns>The blended colors.</returns>
        public static Color Blend(this Color color, Color backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }
    }
}