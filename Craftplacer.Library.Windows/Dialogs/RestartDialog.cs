using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Craftplacer.Library.NativeMethods;

namespace Craftplacer.Library.Windows
{
	/// <summary>
	/// About dialog box used by Microsoft Windows applications.
	/// </summary>
	[Browsable(true)]
	[ToolboxBitmap(typeof(RestartDialog), nameof(RestartDialog) + ".bmp")]
	public class RestartDialog : CommonDialog
	{
		/// <summary>
		/// The name of the application
		/// </summary>
		[Category("Appearance")]
		[Description("Additional text that will be shown in the dialog")]
		public string Text { get; set; } = string.Empty;

		public override void Reset()
		{
			this.Text = string.Empty;
		}

		protected override bool RunDialog(IntPtr hwndOwner)
		{
			Shell32.RestartDialog(hwndOwner, Text, 0x00000002);
			return true;
		}
	}
}