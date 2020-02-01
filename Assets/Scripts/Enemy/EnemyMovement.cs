using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection
{
	Up,
	Down,
	Left,
	Right,
	Count
}

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] Rigidbody2D _rigidBody = null;
	[SerializeField] EnemyAnimator _charAnimator = null;
	Camera _camera = null;

	//[SerializeField] List<BoxCollider2D> _attackColliders = new List<BoxCollider2D>();
	[SerializeField] GameObject _universalAttackBox = null;

	[SerializeField] float _movementSpeed = 5f;

	Vector2 _movementVelocity;
	Vector2 _movementInput;
	float _horzInput;
	float _vertInput;
	MovementDirection _movementDirection;

	Vector2 _mousePos;

	// Start is called before the first frame update
	void Start()
	{
		Debug.Assert(_charAnimator, "_charAnimator not set!");
		_charAnimator.OnAttackStateChangedEvent += OnAttackStateChanged;

		_camera = Camera.main;

		DeactivateAttackColliders();
	}

	void OnAttackStateChanged(bool isAttacking)
	{
		if (isAttacking)
		{
			ActivateAttackCollider();
		}
		else
		{
			DeactivateAttackColliders();
		}
	}

	private void DeactivateAttackColliders()
	{
		//foreach(var col in _attackColliders)
		//{
		//	col.enabled = false;
		//}

		_universalAttackBox.transform.GetChild(0).gameObject.SetActive(false);
	}

	private void ActivateAttackCollider()
	{
		//_attackColliders[(int)_movementDirection].enabled = true;

		_universalAttackBox.transform.GetChild(0).gameObject.SetActive(true);
	}

	void UpdateMovementInput()
	{
		//TODO: create an AI movement here

		_horzInput = 0f; // Input.GetAxisRaw("Horizontal");
		_vertInput = 0f; //Input.GetAxisRaw("Vertical");

		_movementInput = new Vector2(_horzInput, _vertInput);
		_movementVelocity = _movementInput.normalized * _movementSpeed;

		_charAnimator.SetMovementInputValue(_movementInput.magnitude);
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

		_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

		_charAnimator.SetMovementDirection((int)_movementDirection);
	}


	// Update is called once per frame
	void Update()
	{
		if (_charAnimator.GetIsAttacking())
		{
			return;
		}

		UpdateMovementInput();
		UpdateMovementDirection();
	}

	private void FixedUpdate()
	{
		if (_charAnimator.GetIsAttacking())
		{
			return;
		}

		_rigidBody.MovePosition(_rigidBody.position + _movementVelocity * Time.deltaTime);

		Vector2 lookDir = _mousePos - _rigidBody.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

		_universalAttackBox.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
	}
}
