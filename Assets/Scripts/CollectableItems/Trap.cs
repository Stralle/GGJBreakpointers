﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETrapType
{
    Spikes,
    Arrow,
    BuzzSaw,
    Invalid
}

public class Trap : MonoBehaviour, IRepairable
{
    [SerializeField]
    protected ETrapType _trapType = ETrapType.Invalid;

	[SerializeField] ParticleSystem _repareEffect = null;

    [SerializeField]
    protected int _damageDealt = 0;

    [SerializeField] // TODO: _woodCost, _stoneCost, _metalCost
    protected int _junkCost = 0;
	public int JunkCost => _junkCost;

	[SerializeField]
    protected bool _isRepaired = false;
    [SerializeField]
    protected bool _isActive = false;

    [SerializeField]
    protected GameObject _canvas = null;

	[SerializeField] AudioClip _repairSound = null;
	[SerializeField] AudioClip _breakSound = null;

	// Getters and setters
	public int DamageDealt => _damageDealt;

    public bool IsRepaired { get => _isRepaired; set => _isRepaired = value; }

    public bool IsActive { get => _isActive; set => _isActive = value; }

    public virtual ETrapType GetTrapType()
    {
        return _trapType;
    }

    // Virtual methods => Override these
    public virtual void RepairAndSpendResources() { }
    public virtual bool CanDealDamage() { return _isActive; }
    public virtual void DestroyAndReceiveResources() { }

	public virtual bool CanBeRepared()
	{
		return _junkCost <= GameRulesManager.Instance.GetAmountOfResources(EResourceType.Junk);
	}

	public virtual void TriggerTrap(EnemyBase enemy)
	{
		Debug.Assert(false, "Trap::TriggerTrap - Not implemented in " + name);
	}

	public bool PlayerInteract()
	{
		GameRulesManager.Instance.OnTrapFound(this);

		if (IsRepaired)
		{
			DestroyAndReceiveResources();
			if (_repareEffect)
			{
				_repareEffect.Play();
			}
			if (_breakSound)
			{
				SoundManager.Instance.PlayOneTimeSound(_breakSound, 0.4f);
			}
			return true;
		}
		else if (!IsRepaired && CanBeRepared())
		{
			RepairAndSpendResources();
			if (_repareEffect)
			{
				_repareEffect.Play();
			}
			if (_repairSound)
			{
				SoundManager.Instance.PlayOneTimeSound(_repairSound, 0.3f);
			}
			return true;
		}

        return false;
	}

    public void SetUIFeedbackActive(bool _setActive)
    {
        if (_canvas == null)
        {
            Debug.LogError("The canvas is missing!");
            return;
        }

		if (_setActive)
		{
			GameRulesManager.Instance.OnTrapFound(this);
		}
		else
		{
			GameRulesManager.Instance.OnTrapUnreachable();
		}

        _canvas.SetActive(_setActive);
    }

    protected virtual void Start()
    {
        if (_canvas == null)
        {
            Debug.LogError("The trap is missing canvas!");
            return;
        }

        SetUIFeedbackActive(false);
    }
}
