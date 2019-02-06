﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Custom {
	/// <summary>
	/// View to show unit stats and control available of upgrade control
	/// </summary>
	public class UnitView : MonoBehaviour {
		public Text       NameText;
		public Text       HealthText;
		public Selectable UpgradeButton;
		public float      AnimationTime;

		UnitModel _unit;

		void OnValidate() {
			Debug.Assert(NameText,      nameof(NameText));
			Debug.Assert(HealthText,    nameof(HealthText));
			Debug.Assert(UpgradeButton, nameof(UpgradeButton));
		}

		void Start() {
			_unit = GameState.Instance.Repository.Unit;
			_unit.Name.OnChange(name => NameText.text = name);
			_unit.CanUpgrade.OnChange(active => UpgradeButton.interactable = active);
			_unit.CurrentHp.OnChange(_ => UpdateHealth());
			_unit.MaxHp.OnChange(_ => UpdateHealth());
			_unit.IsDead.OnChange(_ => UpdateHealth());

			_unit.UpgradeLevel.OnNextChange(_ => MainThreadRunner.Instance.StartCoroutine(AnimateName()));
		}

		void UpdateHealth() {
			HealthText.text = string.Format(
				"{0}/{1} (is dead? {2})",
				_unit.CurrentHp.Value, _unit.MaxHp.Value, _unit.IsDead.Value
			);
		}

		IEnumerator AnimateName() {
			var t = 0.0f;
			var trans = NameText.transform;
			var initialScale = trans.localScale;
			var animSpeed = 2.0f / AnimationTime;
			while ( t < 1.0f ) {
				trans.localScale = Vector3.Lerp(initialScale, initialScale * 2, t);
				t += Time.deltaTime * animSpeed;
				yield return null;
			}
			t = 0.0f;
			while ( t < 1.0f ) {
				trans.localScale = Vector3.Lerp(initialScale * 2, initialScale, t);
				t += Time.deltaTime * animSpeed;
				yield return null;
			}
		}
	}
}
