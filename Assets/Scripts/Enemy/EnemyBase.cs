using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyType
{
	Knight
}

public class EnemyBase : MonoBehaviour
{
	[SerializeField] ParticleSystem _onHitEffect;
	[SerializeField] ParticleSystem _onWinEffect;

	[SerializeField] int _health = 1;
	public int Health => _health;

	Animator _animator = null;

	[SerializeField] EEnemyType _enemyType = EEnemyType.Knight;

	public void TakeDamage(int damage)
	{
		_onHitEffect.gameObject.SetActive(true);
		_onHitEffect.Play();

		_health -= damage;

		if (Health <= 0)
		{
			// object will be destroyed after anim
			_animator.SetTrigger("isDead");

			EnemyMovementPhases movementPhasesComponent = GetComponent<EnemyMovementPhases>();
			Debug.Assert(movementPhasesComponent, "The enemy is missing EnemyMovementPhases Component");
			if (movementPhasesComponent != null)
			{
				movementPhasesComponent.SetIsDead();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("EnemyBAse: OnTriggerEnter2D");
		// todo::get dumage from trap and apply it
		Trap trap = collision.GetComponent<Trap>();
		if (trap && trap.CanDealDamage())
		{
			TakeDamage(trap.DamageDealt);
		}

		InteractablePart interactible = collision.GetComponent<InteractablePart>();
		if (interactible)
		{
			Loot loot = interactible.GetMainGameObject().GetComponent<Loot>();
			if (loot)
			{
				loot.DestroyByEnemy();
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("EnemyBAse: OnCollisionEnter2D");
		// todo::get dumage from trap and apply it
		Trap trap = collision.gameObject.GetComponent<Trap>();
		if (trap && trap.CanDealDamage())
		{
			TakeDamage(trap.DamageDealt);
		}

		InteractablePart interactible = collision.gameObject.GetComponent<InteractablePart>();
		if (interactible)
		{
			Loot loot = interactible.GetMainGameObject().GetComponent<Loot>();
			if (loot)
			{
				loot.DestroyByEnemy();
			}
		}
	}

	public void SetIsAWinner()
	{
		_onWinEffect.gameObject.SetActive(true);
		_onWinEffect.Play();

		_animator.SetTrigger("isWinner");
		//GameRulesManager.Instance.EndGame();
	}

	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();

		Debug.Assert(_onHitEffect != null, "Attach particles to the enemy!");
		_onHitEffect.gameObject.SetActive(false);

		Debug.Assert(_onWinEffect != null, "Attach particles to the enemy!");
		_onWinEffect.gameObject.SetActive(false);
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
