#pragma warning disable CA1401 // P/Invokes should not be visible

using System;
using System.Runtime.InteropServices;

namespace Craftplacer.Library.NativeMethods
{
    public static class UxTheme
    {
        /// <summary>
        /// Prevents the window caption from being drawn.
        /// </summary>
        public const uint WTNCA_NODRAWCAPTION = 0x00000001;

        /// <summary>
        /// Prevents the system icon from being drawn.
        /// </summary>
        public const uint WTNCA_NODRAWICON = 0x00000002;

        /// <summary>
        /// Prevents the system icon menu from appearing.
        /// </summary>
        public const uint WTNCA_NOSYSMENU = 0x00000004;

        /// <summary>
        /// Prevents mirroring of the question mark, even in right-to-left (RTL) layout.
        /// </summary>
        public const uint WTNCA_NOMIRRORHELP = 0x00000008;

        /// <summary>
        /// A mask that contains all the valid bits.
        /// </summary>
        public const uint WTNCA_VALIDBITS = WTNCA_NODRAWCAPTION | WTNCA_NODRAWICON | WTNCA_NOSYSMENU | WTNCA_NOMIRRORHELP;

        //[DllImport("uxtheme.dll", PreserveSig = false)]
        //public static extern void SetWindowThemeAttribute([In] IntPtr hwnd, [In] WINDOWTHEMEATTRIBUTETYPE eAttribute, [In] ref WTA_OPTIONS pvAttribute, [In] uint cbAttribute);

        [DllImport("uxtheme.dll", PreserveSig = false)]
        public static extern void SetWindowThemeAttribute([In] IntPtr hwnd, [In] uint eAttribute, [In] ref WTA_OPTIONS pvAttribute, [In] uint cbAttribute);

        public enum WINDOWTHEMEATTRIBUTETYPE : uint
        {
            WTA_NONCLIENT
        };

        public struct WTA_OPTIONS
        {
            public uint dwFlags;
            public uint dwMask;
        }
    }
}