/*
	This file is
		© 2022 LisiasT

	And licensed to Kerbal Klinic under:
		* GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

	You should have received a copy of the GNU General Public License 3.0
	along with this product. If not, see <https://www.gnu.org/licenses/>.
*/
using UnityEngine;

using KSP.UI.Screens;

namespace KerbalKlinic.UI
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	internal class ToolbarSupport : MonoBehaviour
	{
		private static ToolbarSupport instance = null;
		internal static ToolbarSupport Instance => instance;
		private ApplicationLauncherButton button;

		private void Awake() {
			DontDestroyOnLoad(this);
			instance = this;
		}

		private void Start() {
			GameEvents.onGUIApplicationLauncherReady.Add(this.OnGUIApplicationLauncherReady);
			GameEvents.onGUIApplicationLauncherDestroyed.Add(this.OnGUIApplicationLauncherDestroyed);
		}

		private void OnDestroy() {
			instance = null;
			this.OnGUIApplicationLauncherDestroyed();	// Force the destruction of the Current Button
			GameEvents.onGUIApplicationLauncherDestroyed.Remove(this.OnGUIApplicationLauncherDestroyed);
			GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIApplicationLauncherReady);
		}

		private void OnGUIApplicationLauncherReady() {
			if (null == this.button)
				this.button = ApplicationLauncher.Instance.AddModApplication(
							this.OnTrue, this.OnFalse
							, this.Dummy, this.Dummy
							, this.Dummy, this.Dummy
							, ApplicationLauncher.AppScenes.SPACECENTER
							, UI.Icons.Button
						)
					;
			this.UpdateIcon();
		}

		internal void UpdateIcon() {
			if (null == KerbalKlinic.Instance) this.button.SetTexture(UI.Icons.Button);
			else this.button.SetTexture(KerbalKlinic.Instance.ShowMenu ? UI.Icons.ButtonOn : UI.Icons.ButtonOff);
		}

		internal void UpdateIcon(bool active, bool available) {
			if (null != KerbalKlinic.Instance && active && available) this.UpdateIcon();
			else this.button.SetTexture(UI.Icons.Button);
		}

		private void OnGUIApplicationLauncherDestroyed() {
			if (null == this.button) return;
			ApplicationLauncher.Instance.RemoveModApplication(this.button);
			Destroy(this.button);
			this.button = null;
		}

		private void Dummy() {
			// Dummy Bears!!! :P
		}

		private void OnTrue() {
			if (null != KerbalKlinic.Instance)		// Better safe than sorry.
				KerbalKlinic.Instance.ShowMenu = true;
			this.UpdateIcon();
		}

		private void OnFalse() {
			if (null != KerbalKlinic.Instance)		// Better safe than sorry.
				KerbalKlinic.Instance.ShowMenu = false;
			this.UpdateIcon();
		}
	}
}
