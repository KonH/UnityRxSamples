using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// Controller to call upgrade method
/// </summary>
public class UnitController : MonoBehaviour {
	public Button UpgradeButton;

	void OnValidate() {
		Debug.Assert(UpgradeButton, nameof(UpgradeButton));
	}

	void Start() {
		UpgradeButton.OnClickAsObservable().Subscribe(_ => GameState.Instance.UpgradeService.Upgrade());
    }
}
