using UnityEngine;

namespace Scripts.Custom {
	/// <summary>
	/// Unit state
	/// </summary>
	public class UnitModel {

		/// <summary>
		/// Min interval between two updates
		/// </summary>
		const float _minUpgradeInterval = 3.0f;

		// Simple read-only wrappers of stats below (to prevent modifications not inside special methods)

		public IReadOnlyObservable<int> UpgradeLevel => _upgradeLevel.AsReadOnly();
		public IReadOnlyObservable<int> CurrentHp    => _currentHp.AsReadOnly();
		public IReadOnlyObservable<int> MaxHp        => _maxHp.AsReadOnly();

		// Computable properties

		public IReadOnlyObservable<string> Name       => _name.AsReadOnly();
		public IReadOnlyObservable<bool>   IsDead     => _isDead.AsReadOnly();
		public IReadOnlyObservable<bool>   CanUpgrade => _isCanUpgrade.AsReadOnly();

		bool IsCanUpgrade => !IsDead.Value && ((_lastUpgradeTime + _minUpgradeInterval) < Time.realtimeSinceStartup);

		float _lastUpgradeTime = float.MinValue;

		string _baseName;
		Observable<int> _upgradeLevel;
		Observable<int> _currentHp;
		Observable<int> _maxHp;

		// additional logics
		Observable<string> _name         = new Observable<string>();
		Observable<bool>   _isDead       = new Observable<bool>();
		Observable<bool>   _isCanUpgrade = new Observable<bool>();

		public UnitModel(string name, int lvl, int hp) {
			_baseName     = name;
			_upgradeLevel = new Observable<int>(lvl);
			_currentHp    = new Observable<int>(hp);
			_maxHp        = new Observable<int>(hp);

			// additional logics
			_upgradeLevel.OnChange(newLvl => _name.Value = $"{_baseName} (lvl {newLvl})");
			_currentHp.OnChange(newHp => {
				if ( newHp <= 0 ) {
					_isDead.Value = true;
				}
			});

			MainThreadRunner.Instance.EveryUpdate(() => _isCanUpgrade.Value = IsCanUpgrade);
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
}
