﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyType
{
	Knight
}

public class EnemyBase : MonoBehaviour
{
	[SerializeField] int _health = 100;
	public int Health => _health;

	Animator _animator = null;

	[SerializeField] EEnemyType _enemyType = EEnemyType.Knight;

	public void TakeDamage(int damage)
	{
		_health -= damage;

		if (Health <= 0)
		{
			_animator.SetTrigger("isDead");
		}
	}

	private void OnCollisionEnter2D(Collision collision)
	{
		// todo::get dumage from trap and apply it
		TakeDamage(100);
	}

	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();
	}

	public void SetMovementPath(EnemyDecisionPoint[] _movementPhases)
	{
		EnemyMovementPhases _movementPhasesComponent = GetComponent<EnemyMovementPhases>();
		Debug.Assert(_movementPhasesComponent, "The enemy is missing EnemyMovementPhases Component");
		if (_movementPhasesComponent == null)
		{
			return;
		}
		_movementPhasesComponent.SetMovementPoints(_movementPhases);
	}

	public void SetInitialPosition(Vector2 pos)
	{
		transform.position = new Vector3(pos.x, pos.y, transform.position.z);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
