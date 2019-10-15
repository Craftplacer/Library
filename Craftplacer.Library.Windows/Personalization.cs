using Microsoft.Win32;
using System.Drawing;
using static Craftplacer.Library.Extensions.ColorExtensions;
using static Craftplacer.Library.Extensions.IntegerExtensions;

namespace Craftplacer.Library.Windows
{
	public static class Personalization
	{
		private const string AccentColorSubKey = "Software\\Microsoft\\Windows\\DWM";
		private const string AccentColorValue = "AccentColor";

		public static Color AccentColor
		{
			get
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AccentColorSubKey))
				{
					int bgr = (int)key.GetValue(AccentColorValue);
					return bgr.ToBgrColor();
				}
			}
			set
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AccentColorSubKey, true))
				{
					int bgr = value.ToBgrInt();
					key.SetValue(AccentColorValue, bgr);
				}
			}
		}

		public static bool LightMode
		{
			get
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
				{
					return (int)key.GetValue("AppsUseLightTheme") == 1 ? true : false;
				}
			}
			set
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true))
				{
					key.SetValue("AppsUseLightTheme", value ? 1 : 0);
				}
			}
		}
	}
}