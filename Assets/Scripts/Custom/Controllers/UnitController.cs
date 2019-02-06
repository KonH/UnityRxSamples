using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Custom {
	/// <summary>
	/// Controller to call upgrade method
	/// </summary>
	public class UnitController : MonoBehaviour {
		public Button UpgradeButton;

		void OnValidate() {
			Debug.Assert(UpgradeButton, nameof(UpgradeButton));
		}

		void Start() {
			UpgradeButton.onClick.AddListener(GameState.Instance.UpgradeService.Upgrade);
		}
	}
}
