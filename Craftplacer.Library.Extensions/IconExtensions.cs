using System.Drawing;
using System.IO;

namespace Craftplacer.Library.Extensions
{
    public static class IconExtensions
    {
        public static Icon GetSize(this Icon icon, int width, int height) => icon.GetSize(new Size(width, height));

        public static Icon GetSize(this Icon icon, Size size)
        {
            using (var memoryStream = new MemoryStream())
            {
                icon.Save(memoryStream);
                memoryStream.Position = 0;
                return new Icon(memoryStream, size);
            }
        }
    }
}
