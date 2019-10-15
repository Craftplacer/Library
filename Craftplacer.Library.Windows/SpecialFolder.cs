using System;

namespace Craftplacer.Library.Windows
{
	public static partial class SpecialFolder
	{
		public static string GetPath(this SpecialFolders specialFolder)
		{
			Guid? folderGuid = GetGuid(specialFolder);
			if (folderGuid.HasValue)
			{
				NativeMethods.Shell32.SHGetKnownFolderPath(folderGuid.Value, 0, IntPtr.Zero, out string path);
				return path;
			}
			return null;
		}

		private static Guid? GetGuid(this SpecialFolders specialFolder) => Guids.ContainsKey(specialFolder) ? (Guid?)Guids[specialFolder] : null;
	}
}