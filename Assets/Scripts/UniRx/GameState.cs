namespace Scripts.UniRx {
	/// <summary>
	/// Simple single-instance game state 
	/// </summary>
	public class GameState {
		public static GameState Instance { get; } = new GameState();

		public UnitRepository Repository     { get; }
		public DamageService  DamageService  { get; }
		public UpgradeService UpgradeService { get; }

		public GameState() {
			Repository     = new UnitRepository();
			DamageService  = new DamageService(Repository);
			UpgradeService = new UpgradeService(Repository);
		}
	}
}
