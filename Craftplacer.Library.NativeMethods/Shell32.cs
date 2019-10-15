#pragma warning disable CA1401 // P/Invokes should not be visible

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Craftplacer.Library.NativeMethods
{
    public static class Shell32
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHEmptyRecycleBin(IntPtr hWnd, string pszRootPath, uint dwFlags);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int ShellAbout(IntPtr hWnd, string szApp, string szOtherStuff, IntPtr hIcon);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int RestartDialog(IntPtr hwnd, string pszPrompt, uint dwReturn);

        public enum NotifyIconMessage : uint
        {
            /// <summary>
            /// Adds an icon to the status area. The icon is given an identifier in the NOTIFYICONDATA structure pointed to by lpdata—either through its uID or guidItem member. This identifier is used in subsequent calls to Shell_NotifyIcon to perform later actions on the icon.
            /// </summary>
            NIM_ADD = 0x00000000,

            /// <summary>
            /// Modifies an icon in the status area. NOTIFYICONDATA structure pointed to by lpdata uses the ID originally assigned to the icon when it was added to the notification area (<see cref="NIM_ADD"/>) to identify the icon to be modified.
            /// </summary>
            NIM_MODIFY = 0x00000001,

            /// <summary>
            /// Deletes an icon from the status area. NOTIFYICONDATA structure pointed to by lpdata uses the ID originally assigned to the icon when it was added to the notification area (<see cref="NIM_ADD"/>) to identify the icon to be deleted.
            /// </summary>
            NIM_DELETE = 0x00000002,

            /// <summary>
            /// Shell32.dll version 5.0 and later only. Returns focus to the taskbar notification area. Notification area icons should use this message when they have completed their UI operation. For example, if the icon displays a shortcut menu, but the user presses ESC to cancel it, use <see cref="NIM_SETFOCUS"/> to return focus to the notification area.
            /// </summary>
            NIM_SETFOCUS = 0x00000003,

            /// <summary>
            /// Shell32.dll version 5.0 and later only. Instructs the notification area to behave according to the version number specified in the uVersion member of the structure pointed to by lpdata. The version number specifies which members are recognized.
            ///
            /// <see cref="NIM_SETVERSION"/> must be called every time a notification area icon is added (<see cref="NIM_ADD"/>)>. It does not need to be called with <see cref="NIM_MODIFY"/>.The version setting is not persisted once a user logs off.
            /// </summary>
            NIM_SETVERSION = 0x00000004,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NOTIFYICONDATA
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint uID;
            public uint uFlags;
            public uint uCallbackMessage;
            public IntPtr hIcon;
            public char[] szTip;
            public uint dwState;
            public uint dwStateMask;
            public char[] szInfo;
            public NOTIFYICONDATAUNION DUMMYUNIONNAME;
            public char[] szInfoTitle;
            public uint dwInfoFlags;
            public Guid guidItem;
            public IntPtr hBalloonIcon;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NOTIFYICONDATAUNION
        {
            public uint uTimeout;
            public uint uVersion;
        }
    }
}