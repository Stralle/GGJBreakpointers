using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : Trap
{
    [SerializeField]
    Sprite _brokenArrowSprite = null;

    [SerializeField]
    Sprite _activeArrowSprite = null;

	[SerializeField] Animator _crossbowAnimator = null;

	SpriteRenderer _spriteRendererComponent = null;

	override protected void Start()
	{
		base.Start();

		_spriteRendererComponent = GetComponent<SpriteRenderer>();
		Debug.Assert(_spriteRendererComponent, "Missing sprite renderer.");

		Debug.Assert(_crossbowAnimator, "Missing crossbow animator");

		ChangeTrapView();
	}

	public void ChangeTrapView()
	{
		if (_isRepaired)
		{
			_spriteRendererComponent.sprite = _activeArrowSprite;
			_crossbowAnimator.gameObject.SetActive(true);
			_crossbowAnimator.SetBool("IsFired", false);
		}
		else
		{
			_crossbowAnimator.gameObject.SetActive(false);
			_spriteRendererComponent.sprite = _brokenArrowSprite;
		}
	}

	public override void RepairAndSpendResources()
	{
		if (!_isRepaired)
		{
			_isRepaired = true;
			_isActive = true;
			ChangeTrapView();

			GameRulesManager.Instance.ResourcesSpent(EResourceType.Junk, _junkCost);
			// TODO: Decrease other player's resources
		}
	}

	public override void DestroyAndReceiveResources()
	{
		if (_isRepaired)
		{
			_isRepaired = false;
			_isActive = false;
			ChangeTrapView();

			GameRulesManager.Instance.ResourcesCollected(EResourceType.Junk, _junkCost);
			// TODO: Increase other player's resources
		}
	}

	// TODO: make fire logic _crossbowAnimator.SetBool("IsFired", true);

	public override bool CanDealDamage()
	{
		return IsActive;
	}
}
