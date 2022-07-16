/*
	This file is
		© 2022 LisiasT

	And licensed to zer0Kerbal under:
		* GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

	You should have received a copy of the GNU General Public License 3.0
	along with this product. If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.IO;

using UnityEngine;

using RUI.Icons.Selectable;

namespace KerbalKlinic.UI
{
	internal static class Icons
	{
		private const string ICONSDIR = "Icons";

		private static Texture2D _Button = null;
		internal static Texture2D Button = _Button ?? (_Button = LoadFromFile("button"));

		private static Texture2D _ButtonOff = null;
		internal static Texture2D ButtonOff = _ButtonOff ?? (_ButtonOff = LoadFromFile("button_off"));

		private static Texture2D _ButtonOn = null;
		internal static Texture2D ButtonOn = _ButtonOn ?? (_ButtonOn = LoadFromFile("button_on"));

		private static Texture2D LoadFromFile(string iconFile)
		{
			string root = Path.GetDirectoryName(typeof(Icons).Assembly.Location);
			string pathname = Path.Combine(root,"PluginData", ICONSDIR, iconFile+".png");
			Texture2D t = new(2, 2, TextureFormat.RGBA32, false);
			ImageConversion.LoadImage(t, File.ReadAllBytes(pathname));
			return t;
		}

		private static Icon _Icon = null;
		internal static Icon Icon = _Icon ?? (_Icon = GenIcon());
		private static Icon GenIcon()
		{
			Icon icon = new Icon("ButtonIcon", UI.Icons.ButtonOff, UI.Icons.ButtonOn);
			return icon;
		}
	}
}
