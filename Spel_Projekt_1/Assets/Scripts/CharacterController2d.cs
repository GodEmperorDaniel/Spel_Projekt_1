﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

public class CharacterController2d : MonoBehaviour {
	static readonly float _r = Mathf.Cos(Mathf.PI / 8) * Mathf.Cos(Mathf.PI / 8) / (Mathf.Sin(Mathf.PI / 8) * Mathf.Sin(Mathf.PI / 8));
	static readonly float _invSqr2 = 1 / Mathf.Sqrt(2);
  
	public enum DirectionType {
		full360,
		directions8,
		directions4,
	}
	public enum SpeedType {
		smooth,
		toggle
	}

	[FormerlySerializedAs("MovementSpeed")]
	public float movementSpeed = 2;
	public float stepSize = 0.5f;
	[Range(0,1)]
	public float deadZone = 0.1f;
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public string interactionButton = "InteractionButton";
	public DirectionType directionType = DirectionType.directions4;
	public SpeedType speedType = SpeedType.toggle;

	[FormerlySerializedAs("Ani")]
	public Animator animator;
	public string animatorMovementBool = "movement";
	public string animatorHorizontalFloat = "horimovement";
	public string animatorVerticalFloat = "vertimovement";

	private float _stepLeft;
	private Vector2 _stepDir;

	private void Start() {
		if (animator == null) {
			animator = GetComponent<Animator>();
		}
	}

	private void Update() {
		if (_stepLeft > 0) {
			_stepLeft -= Time.deltaTime;
		} else {
			ReadInput();
		}

		Translate();
	}

	private void ReadInput() {
		var h = Input.GetAxisRaw(horizontalAxis);
		var v = Input.GetAxisRaw(verticalAxis);

		if (h * h + v * v > deadZone * deadZone) {
			_stepLeft = stepSize;
			switch (directionType) {
				case DirectionType.full360:
					_stepDir = new Vector2(h, v);
					_stepDir.Normalize();
					break;
				case DirectionType.directions4:
					if (h * h > v * v) {
						_stepDir = new Vector2(Mathf.Sign(h), 0);
					} else {
						_stepDir = new Vector2(0, Mathf.Sign(v));
					}
					break;
				case DirectionType.directions8:
					if (h * h > _r * v * v) {
						_stepDir = new Vector2(Mathf.Sign(h), 0);
					} else if (v * v > _r * h * h) {
						_stepDir = new Vector2(0, Mathf.Sign(v));
					} else {
						_stepDir = new Vector2(Mathf.Sign(h) * _invSqr2, Mathf.Sign(v) * _invSqr2);
					}
					break;
			}

			if (speedType == SpeedType.smooth) {
				_stepDir *= Mathf.Sqrt(h * h + v * v);
			}

			SetAnimatorVariables(true);
		} else {
			_stepDir = Vector2.zero;
		}
	}

	private void Translate() {
		transform.Translate(_stepDir * movementSpeed * Time.deltaTime);
	}

	private void SetAnimatorVariables(bool moving) {
		if (animator != null) {
			animator.SetBool(animatorMovementBool, moving);
			animator.SetFloat(animatorHorizontalFloat, _stepDir.x);
			animator.SetFloat(animatorVerticalFloat, _stepDir.y);
		}
	}

	public bool GetInteractionKey() {
		return isActiveAndEnabled && Input.GetAxisRaw(interactionButton) > 0;
	}
}
