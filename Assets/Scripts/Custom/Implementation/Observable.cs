using System;
using System.Collections.Generic;

namespace Scripts.Custom {
	/// <summary>
	/// Straight-forward approach to observable values
	/// Not yet working with destroy and scene changes
	/// </summary>
	public sealed class Observable<T> : IReadOnlyObservable<T> where T : IComparable<T> {
		public T Value {
			get {
				return _value;
			}
			set {
				if ( value.Equals(_value) ) {
					return;
				}
				_value = value;
				FireChanged();
			}
		}

		T _value;
		List<Action<T>> _handlers = new List<Action<T>>();

		public Observable(T initial = default(T)) {
			_value = initial;
		}

		public void OnChange(Action<T> callback) {
			callback.Invoke(_value);
			OnNextChange(callback);
		}

		public void OnNextChange(Action<T> callback) {
			_handlers.Add(callback);
		}

		public IReadOnlyObservable<T> AsReadOnly() {
			return this as IReadOnlyObservable<T>;
		}

		void FireChanged() {
			foreach ( var handler in _handlers ) {
				handler.Invoke(_value);
			}
		}
	}
}
