namespace Scripts.Custom {
	/// <summary>
	/// Upgrade logics: increase level and max hp, restore current hp
	/// </summary>
	public class UpgradeService {
		readonly UnitRepository _repo;

		public UpgradeService(UnitRepository repo) {
			_repo = repo;
		}

		public void Upgrade() {
			var unit = _repo.Unit;
			unit.IncreaseLevel();
			unit.UpdateMaxHp((int)(unit.MaxHp.Value * 1.5));
			unit.RestoreCurrentHp();
		}
	}
}
