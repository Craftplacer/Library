using Craftplacer.Library.Windows.Exceptions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using static Craftplacer.Library.NativeMethods.User32;

namespace Craftplacer.Library.Windows
{
    /// <summary>
    /// A managed class for handling a Win32 window.
    /// </summary>
    public struct Window
    {
        /// <summary>
        /// The handle of this window
        /// </summary>
        public readonly IntPtr Handle;

        public Window(IntPtr handle)
        {
            if (!IsWindow(handle))
            {
                throw new NotWindowHandleException("The specified handle is not a window.");
            }

            Handle = handle;
        }

        public Window(IWin32Window window) : this(window.Handle)
        {
        }

        public enum IconType
        {
            /// <summary>
            /// Retrieve the large icon for the window.
            /// </summary>
            Big = 1,

            /// <summary>
            /// Retrieve the small icon for the window.
            /// </summary>
            Small = 0,

            /// <summary>
            /// Retrieves the small icon provided by the application. If the application does not provide one, the system uses the system-generated icon for that window.
            /// </summary>
            AutoSmall = 2
        }

        /// <summary>
        /// The window the user is currently working with.
        /// </summary>
        public static Window ForegroundWindow
        {
            get => new Window(GetForegroundWindow());
            set => SetForegroundWindow(value.Handle);
        }

        public Icon Icon
        {
            get
            {
                var icon = this.GetIcon(IconType.Big);

                if (icon == null)
                    this.GetIcon(IconType.AutoSmall);

                if (icon == null)
                    this.GetIcon(IconType.Small);

                if (icon == null)
                {
                    try
                    {
                        var filePath = Process.MainModule.FileName;

                        Debug.WriteLine(filePath);

                        if (!string.IsNullOrWhiteSpace(filePath))
                        {
                            return Icon.ExtractAssociatedIcon(filePath);
                        }
                    }
                    catch (Win32Exception)
                    {
                    }
                }

                return icon;
            }
        }

        /// <summary>
        /// Determines whether the system considers that a specified application is not responding. An application is considered to be not responding if it is not waiting for input, is not in startup processing, and has not called PeekMessage within the internal timeout period of 5 seconds.
        /// </summary>
        public bool IsHung => IsHungAppWindow(Handle);

        /// <summary>
        /// Determines whether the window is maximized.
        /// </summary>
        public bool IsMaximized
        {
            get => IsZoomed(Handle);
            set
            {
                if (value)
                {
                    ShowWindow(Handle, ShowWindowCommand.SW_SHOWMAXIMIZED);
                }
                else
                {
                    ShowWindow(Handle, ShowWindowCommand.SW_RESTORE);
                }
            }
        }

        /// <summary>
        /// Determines whether the window is minimized.
        /// </summary>
        public bool IsMinimized
        {
            get => IsIconic(Handle);
            set
            {
                if (value)
                {
                    CloseWindow(Handle);
                }
                else
                {
                    ShowWindow(Handle, ShowWindowCommand.SW_RESTORE);
                }
            }
        }

        public Process Process
        {
            get
            {
                GetWindowThreadProcessId(Handle, out var id);
                return Process.GetProcessById(id.ToInt32());
            }
        }

        public bool IsVisible => IsWindowVisible(Handle);

        /// <summary>
        /// The window's parent/owner.
        /// </summary>
        public Window Parent
        {
            get => new Window(GetParent(Handle));
            set => SetParent(Handle, value.Handle);
        }

        public RECT Rectangle
        {
            get
            {
                GetWindowRect(Handle, out var rectangle);
                return rectangle;
            }
        }

        public string Text
        {
            set => SetWindowText(Handle, value);
            get
            {
                int length = GetWindowTextLength(Handle);
                var builder = new StringBuilder(length);

                GetWindowText(Handle, builder, length + 1);

                return builder.ToString();
            }
        }

        public static IReadOnlyList<Window> GetWindows(Func<Window, IntPtr, bool> predicate)
        {
            var windows = new List<Window>();

            EnumWindows(delegate (IntPtr window, IntPtr param)
            {
                if (predicate(window, param))
                    windows.Add(window);

                return true;
            }, IntPtr.Zero);

            return windows;
        }

        public static IReadOnlyList<Window> GetWindows() => GetWindows((w, p) => true);

        /// <summary>
        /// Will attempt to close the window, if some amount of time has passed and the window didn't close yet, the OS will ask the user (see Task Manager's "End Task" button) if the program should be force closed.
        /// This won't happen if <paramref name="force"/> is set to <see cref="true"/>, and the window will be closed immediately.
        /// </summary>
        /// <param name="shutdown">Unknown, should be set to <see cref="false"/>.</param>
        /// <param name="force">If the program will be forcibly closed.</param>
        public bool End(bool shutdown = false, bool force = false) => EndTask(Handle, shutdown, force);

        public override bool Equals(object obj)
        {
            var handle = IntPtr.Zero;

            if (obj is IntPtr h)
                handle = h;
            else if (obj is Window w)
                handle = w.Handle;

            if (handle == IntPtr.Zero)
                return false;

            return Handle == handle;
        }

        /// <summary>
        /// Flashes the window.
        /// </summary>
        /// <param name="flags">The flash status.</param>
        /// <param name="count">The number of times to flash the window.</param>
        /// <param name="timeout">The rate at which the window is to be flashed, in milliseconds. If <paramref name="timeout"/> is 0, the function uses the default cursor blink rate.</param>
        public void Flash(FlashWindow flags, uint count, uint timeout = 0)
        {
            var flashInfo = new FLASHWINFO
            {
                cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO)),
                dwFlags = (uint)flags,
                dwTimeout = timeout,
                hwnd = this.Handle,
                uCount = count
            };

            FlashWindowEx(ref flashInfo);
        }

        public override int GetHashCode() => Handle.GetHashCode();

        public Icon GetIcon(IconType type = IconType.AutoSmall)
        {
            var handle = SendMessage(Handle, 0x007F, (int)type, IntPtr.Zero);

            if (handle == IntPtr.Zero)
                return null;

            return Icon.FromHandle(handle);
        }

        /// <summary>
        /// The focus will be switched to this window.
        /// </summary>
        /// <param name="altTab">If it has been called by the Alt+Tab menu</param>
        public void SwitchTo(bool altTab = false) => SwitchToThisWindow(Handle, altTab);

        public static bool operator ==(Window left, Window right) => left.Equals(right);

        public static bool operator !=(Window left, Window right) => !(left == right);

        //public static implicit operator IntPtr(Window window) => window.Handle;

        public static implicit operator Window(IntPtr handle) => new Window(handle);
    }
}