using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// View to show unit stats and control available of upgrade control
/// </summary>
public class UnitView : MonoBehaviour {
	public Text       NameText;
	public Text       HealthText;
	public Selectable UpgradeButton;

	UnitModel _unit;

	void OnValidate() {
		Debug.Assert(NameText,      nameof(NameText));
		Debug.Assert(HealthText,    nameof(HealthText));
		Debug.Assert(UpgradeButton, nameof(UpgradeButton));
	}

	void Start() {
		_unit = GameState.Instance.Repository.Unit;
		_unit.Name.SubscribeToText(NameText);
		_unit.CanUpgrade.Subscribe(active => UpgradeButton.interactable = active);
		_unit.CurrentHp.Subscribe(_ => UpdateHealth());
		_unit.MaxHp    .Subscribe(_ => UpdateHealth());
		_unit.IsDead   .Subscribe(_ => UpdateHealth());
	}

	void UpdateHealth() {
		HealthText.text = string.Format(
			"{0}/{1} (is dead? {2})",
			_unit.CurrentHp.Value, _unit.MaxHp.Value, _unit.IsDead.Value
		);
	}
}
