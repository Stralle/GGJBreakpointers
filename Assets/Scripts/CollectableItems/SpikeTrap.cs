using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{ 
    [SerializeField]
    Sprite _brokenSpikeSprite = null;
    [SerializeField]
    Sprite _activeSpikeSprite = null;

    SpriteRenderer _spriteRendererComponent = null;

    private void Start()
    {
        _spriteRendererComponent = GetComponent<SpriteRenderer>();
        Debug.Assert(_spriteRendererComponent, "Missing sprite renderer.");

        if (_isRepaired)
        {
            _spriteRendererComponent.sprite = _activeSpikeSprite;
        }
        else
        {
            _spriteRendererComponent.sprite = _brokenSpikeSprite;
        }
    }

    public void ChangeSpikeSprite()
    {
        if (_isRepaired)
        {
            _spriteRendererComponent.sprite = _activeSpikeSprite;
        }
        else
        {
            _spriteRendererComponent.sprite = _brokenSpikeSprite;
        }
    }

    public override void RepairAndSpendResources()
    {
        if (!_isRepaired)
        {
            _isRepaired = true;
			_isActive = true;
            ChangeSpikeSprite();
            
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
            ChangeSpikeSprite();

            GameRulesManager.Instance.ResourcesCollected(EResourceType.Junk, _junkCost);
            // TODO: Increase other player's resources
        }
    }

    public override bool CanDealDamage()
    {
        return IsActive;
    }
}
