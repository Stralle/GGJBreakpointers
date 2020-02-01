using System.Collections;
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

    [SerializeField]
    int _junkNumber = 0;

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

		GameRulesManager.Instance.ResourcesCollected(EResourceType.Junk, _junkNumber);

		// it will remove object after playing the animation in DestroyStateBehavior
		_animator.SetTrigger("Destroy");
	}

	public EDestructibleType GetDestructibleType()
	{
		return _destructibleType;
	}

	public void DestroyByEnemy()
	{
		if (_isDestroyed)
		{
			return;
		}

		_isDestroyed = true;

		// it will remove object after playing the animation in DestroyStateBehavior
		_animator.SetTrigger("Destroy");
	}
}
