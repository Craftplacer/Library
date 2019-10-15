using System;
using System.IO;
using static Craftplacer.Library.NativeMethods.Shell32;

namespace Craftplacer.Library.Windows
{
	public static class RecycleBin
	{
		public static void Empty(IntPtr handle, RecycleBinEmptyFlags flags = RecycleBinEmptyFlags.None) => Empty(handle, string.Empty, flags);

		public static void Empty(RecycleBinEmptyFlags flags = RecycleBinEmptyFlags.None) => Empty(string.Empty, flags);

		public static void Empty(DriveInfo info, RecycleBinEmptyFlags flags = RecycleBinEmptyFlags.None) => Empty(IntPtr.Zero, info, flags);

		public static void Empty(string path, RecycleBinEmptyFlags flags = RecycleBinEmptyFlags.None) => Empty(IntPtr.Zero, path, flags);

		public static void Empty(IntPtr handle, DriveInfo info, RecycleBinEmptyFlags flags = RecycleBinEmptyFlags.None) => Empty(handle, info.RootDirectory.FullName, flags);

		public static void Empty(IntPtr handle, string path, RecycleBinEmptyFlags flags = RecycleBinEmptyFlags.None) => SHEmptyRecycleBin(handle, path, (uint)flags);
	}
}