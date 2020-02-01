﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EResourceType
{
	Junk,
	Size
}

public class Loot : MonoBehaviour, IDestructible
{
	[SerializeField] EDestructibleType _destructibleType = EDestructibleType.Table;

	[SerializeField] Animator _animator = null;

	bool _isDestroyed = false;

	private void Awake()
	{
		Debug.Assert(_animator != null);
		_animator.SetInteger("ItemType", (int)_destructibleType);
	}

	public void DestroyAndGetResources()
	{
		if (_isDestroyed)
		{
			return;
		}

		_isDestroyed = true;

		GameRulesManager.Instance.ResourcesCollected(EResourceType.Junk, 1);

		// it will remove object after playing the animation in DestroyStateBehavior
		_animator.SetTrigger("Destroy");
	}

	public EDestructibleType GetDestructibleType()
	{
		return _destructibleType;
	}
}
