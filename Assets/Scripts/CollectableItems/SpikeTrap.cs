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

    public void SetSpikeTrapActive(bool _setActive)
    {
        if (_setActive)
        {
            _spriteRendererComponent.sprite = _activeSpikeSprite;
        }
        else
        {
            _spriteRendererComponent.sprite = _brokenSpikeSprite;
        }
    }
}
