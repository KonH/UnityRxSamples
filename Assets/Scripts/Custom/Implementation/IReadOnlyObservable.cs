using System;

namespace Scripts.Custom {
	public interface IReadOnlyObservable<T> where T : IComparable<T> {
		T Value { get; }
		void OnChange(Action<T> callback);
		void OnNextChange(Action<T> callback);
	}
}
