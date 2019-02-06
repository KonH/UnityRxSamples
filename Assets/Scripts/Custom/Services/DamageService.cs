using UnityEngine;

namespace Scripts.Custom {
	/// <summary>
	/// Give damage to unit each N seconds, stop trying when unit is dead
	/// </summary>
	public class DamageService {
		float _timer = 0.0f;

		public DamageService(UnitRepository repo) {
			var unit = repo.Unit;
			MainThreadRunner.Instance.EveryUpdate(() => {
				if ( unit.IsDead.Value ) {
					return;
				}
				_timer += Time.deltaTime;
				if ( _timer > 0.25f ) {
					_timer = 0.0f;
					unit.ReceiveDamage(5);
				}
			});
		}
	}
}
