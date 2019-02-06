namespace Scripts.Custom {
	/// <summary>
	/// One model in-memory repository (to provide single instance to controllers, services and views)
	/// </summary>
	public class UnitRepository {
		public UnitModel Unit { get; } = new UnitModel("Unit", 1, 100);
	}
}
