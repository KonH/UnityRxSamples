using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadRunner : MonoBehaviour {
	public static MainThreadRunner Instance {
		get {
			if ( !_instance ) {
				var go = new GameObject("[MainThreadRunner]");
				_instance = go.AddComponent<MainThreadRunner>();
			}
			return _instance;
		}
	}

	static MainThreadRunner _instance;

	List<Action> _updateCallbacks = new List<Action>();

	public void EveryUpdate(Action callback) {
		_updateCallbacks.Add(callback);
	}
}
