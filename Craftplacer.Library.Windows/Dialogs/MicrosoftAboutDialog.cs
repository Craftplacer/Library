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
	[ToolboxBitmap(typeof(MicrosoftAboutDialog), nameof(MicrosoftAboutDialog) + ".bmp")]
	public class MicrosoftAboutDialog : CommonDialog
	{
		/// <summary>
		/// The name of the application
		/// </summary>
		[Category("Appearance")]
		[Description("The name of the application")]
		public string Application { get; set; } = string.Empty;

		/// <summary>
		/// Additional text that will be shown in the dialog
		/// </summary>
		[Category("Appearance")]
		[Description("Additional text that will be shown in the dialog")]
		public string Text { get; set; } = string.Empty;

		/// <summary>
		/// The icon assiociated with the application
		/// </summary>
		[Category("Appearance")]
		[Description("The icon assiociated with the application")]
		public Icon Icon { get; set; } = null;

		public override void Reset()
		{
			Icon = null;
			Text = string.Empty;
			Application = string.Empty;
		}

		protected override bool RunDialog(IntPtr hwndOwner)
		{
			Shell32.ShellAbout(hwndOwner, Application, Text, Icon?.Handle ?? IntPtr.Zero);
			return true;
		}
	}
}