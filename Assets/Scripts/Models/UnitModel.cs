using System;
using UnityEngine;
using UniRx;

/// <summary>
/// Unit state
/// </summary>
public class UnitModel {

	/// <summary>
	/// Min interval between two updates
	/// </summary>
	const float _minUpgradeInterval = 3.0f;

	// Simple read-only wrappers of stats below (to prevent modifications not inside special methods)

	public ReadOnlyReactiveProperty<int>    UpgradeLevel => _upgradeLevel.ToReadOnlyReactiveProperty();
	public ReadOnlyReactiveProperty<int>    CurrentHp    => _currentHp.ToReadOnlyReactiveProperty();
	public ReadOnlyReactiveProperty<int>    MaxHp        => _maxHp.ToReadOnlyReactiveProperty();

	// Computable properties

	public ReadOnlyReactiveProperty<string> Name         => _upgradeLevel.Select(lvl => $"{_baseName} (lvl {lvl})").ToReadOnlyReactiveProperty();
	public ReadOnlyReactiveProperty<bool>   IsDead       => CurrentHp.Select(hp => hp <= 0).ToReadOnlyReactiveProperty();
	public ReadOnlyReactiveProperty<bool>   CanUpgrade   => Observable.EveryUpdate().Select(_ => IsCanUpgrade).ToReadOnlyReactiveProperty(); // Not sure about this approach

	bool IsCanUpgrade => !IsDead.Value && ((_lastUpgradeTime + _minUpgradeInterval) < Time.realtimeSinceStartup);

	float _lastUpgradeTime = float.MinValue;

	string                _baseName;
	ReactiveProperty<int> _upgradeLevel;
	ReactiveProperty<int> _currentHp;
	ReactiveProperty<int> _maxHp;

	public UnitModel(string name, int lvl, int hp) {
		_baseName     = name;
		_upgradeLevel = new ReactiveProperty<int>(lvl);
		_currentHp    = new ReactiveProperty<int>(hp);
		_maxHp        = new ReactiveProperty<int>(hp);
	}

	// Model modification methods

	public void ReceiveDamage(int amount) {
		_currentHp.Value = Mathf.Clamp(_currentHp.Value - amount, 0, _maxHp.Value);
	}

	public void IncreaseLevel() {
		_upgradeLevel.Value++;
		_lastUpgradeTime = Time.realtimeSinceStartup;
	}

	public void UpdateMaxHp(int value) {
		_maxHp.Value = value;
	}

	public void RestoreCurrentHp() {
		_currentHp.Value = _maxHp.Value;
	}
}
