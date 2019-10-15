using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using static Craftplacer.Library.NativeMethods.Shell32;

namespace Craftplacer.Library.Windows.Dialogs
{
	[Browsable(true)]
	[DefaultProperty(nameof(IconPath))]
	[ToolboxBitmap(typeof(IconDialog), nameof(IconDialog) + ".bmp")]
	public class IconDialog : CommonDialog
	{
		public IconDialog() => Reset();

		public string IconPath { get; set; }

		public int IconIndex { get; set; }

		public override void Reset()
		{
			IconPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
			IconIndex = 0;
		}

		protected override bool RunDialog(IntPtr hwndOwner)
		{
			var stringBuilder = new StringBuilder(IconPath, 500);

			int iconIndex = IconIndex;
			int returnValue = PickIconDlg(hwndOwner, stringBuilder, stringBuilder.Length + 1, ref iconIndex);

			if (returnValue == 1)
			{
				IconPath = stringBuilder.ToString();
				IconIndex = iconIndex;

				return true;
			}

			return false;
		}
	}
}