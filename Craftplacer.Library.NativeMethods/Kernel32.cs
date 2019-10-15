#pragma warning disable CA1401 // P/Invokes should not be visible

using System.Runtime.InteropServices;

namespace Craftplacer.Library.NativeMethods
{
    public static class Kernel32
    {
        [DllImport("Kernel32.dll")]
        public static extern bool Beep(uint frequency, uint duration);
    }
}