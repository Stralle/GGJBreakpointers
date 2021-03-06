﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	Rigidbody2D _rigidBody = null;
	[SerializeField] CharacterAnimator _charAnimator = null;

	[SerializeField] float _movementSpeed = 5f;

	Vector2 _movementVelocity;
	Vector2 _movementInput;
	float _horzInput;
	float _vertInput;
	MovementDirection _movementDirection;

	bool _canMove = true;

	// Start is called before the first frame update
	void Start()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		Debug.Assert(_rigidBody, "Missing the Rigidbody2D Component");
		Debug.Assert(_charAnimator, "_charAnimator not set!");

		GameRulesManager.Instance.OnOneSecondLeft += OnOneSecondLeft;
	}

	void UpdateMovementInput()
	{
		_horzInput = Input.GetAxisRaw("Horizontal");
		_vertInput = Input.GetAxisRaw("Vertical");

		_movementInput = new Vector2(_horzInput, _vertInput);
		_movementVelocity = _movementInput.normalized * _movementSpeed;

		_charAnimator.SetMovementInputValue(_movementInput);
	}

	void OnOneSecondLeft()
	{
		_canMove = false;
		// Reseting the input since it won't be updated anymore after this
		_movementVelocity = Vector2.zero;
		_horzInput = 0;
		_vertInput = 0;
		_movementInput = Vector2.zero;
		_charAnimator.SetMovementInputValue(_movementInput);
		_charAnimator.OnOneSecondLeft();
	}

	void UpdateMovementDirection()
	{
		if (Mathf.Abs(_horzInput) > Mathf.Abs(_vertInput))
		{
			if (_horzInput > 0)
			{
				_movementDirection = MovementDirection.Right;
			}
			else if (_horzInput < 0)
			{
				_movementDirection = MovementDirection.Left;
			}
		}
		else if (Mathf.Abs(_horzInput) < Mathf.Abs(_vertInput))
		{
			if (_vertInput > 0)
			{
				_movementDirection = MovementDirection.Up;
			}
			else if (_vertInput < 0)
			{
				_movementDirection = MovementDirection.Down;
			}
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (_canMove)
		{
			UpdateMovementInput();
			UpdateMovementDirection();
		}
	}

	private void FixedUpdate()
	{
		_rigidBody.MovePosition(_rigidBody.position + _movementVelocity * Time.deltaTime);
	}
}