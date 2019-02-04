using System;
using UniRx;

/// <summary>
/// Give damage to unit each N seconds, stop trying when unit is dead
/// </summary>
public class DamageService {
	public DamageService(UnitRepository repo) {
		var unit = repo.Unit;
		var disposable = Observable.Timer(TimeSpan.FromSeconds(0.25)).Repeat().Subscribe(_ => unit.ReceiveDamage(5));
		unit.IsDead.Where(isDead => isDead).Subscribe(_ => disposable.Dispose());
	}
}
