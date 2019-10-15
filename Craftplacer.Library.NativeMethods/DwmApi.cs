#pragma warning disable CA1401 // P/Invokes should not be visible
#pragma warning disable CA1815 // Override equals and operator equals on value types

using System;
using System.Runtime.InteropServices;

namespace Craftplacer.Library.NativeMethods
{
    public static class DwmApi
    {
        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        public struct DWM_COLORIZATION_PARAMS
        {
            public uint clrColor;
            public uint clrAfterGlow;
            public uint nIntensity;
            public uint clrAfterGlowBalance;
            public uint clrBlurBalance;
            public uint clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;

            public MARGINS(int all) : this(all, all, all, all)
            {
            }

            public MARGINS(int leftWidth, int rightWidth, int topHeight, int bottomHeight)
            {
                this.leftWidth = leftWidth;
                this.rightWidth = rightWidth;
                this.topHeight = topHeight;
                this.bottomHeight = bottomHeight;
            }
        }
    }
}