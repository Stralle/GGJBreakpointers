using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
	[SerializeField] Animator _animator = null;

	public event Action<bool> OnAttackStateChangedEvent = delegate {};

	public void SetIsAttacking(bool attacking)
	{
		_animator.SetBool("isAttacking", attacking);

		OnAttackStateChangedEvent.Invoke(attacking);
	}

	public void SetMovementInputValue(float movementInput)
	{
		_animator.SetFloat("MovementInput", movementInput);
	}

	public void SetMovementDirection(int dir)
	{
		_animator.SetInteger("MovementDirection", dir);
	}

	public bool GetIsAttacking()
	{
		return _animator.GetBool("isAttacking");
	}

	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();
		Debug.Assert(_animator, "CharacterAnimator cann't find animator!");
	}

	// Update is called once per frame
	void Update()
	{

	}
}
