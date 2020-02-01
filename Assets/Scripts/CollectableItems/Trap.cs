using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETrapType
{
    Spikes,
    Invalid
}

public class Trap : MonoBehaviour, IRepairable
{
    [SerializeField]
    protected ETrapType _trapType = ETrapType.Invalid;

    [SerializeField]
    protected Animator _animator = null;

    [SerializeField]
    protected int _damageDealt = 0;

    [SerializeField] // TODO: _woodCost, _stoneCost, _metalCost
    protected int _junkCost = 0;

    [SerializeField]
    protected bool _isRepaired = false;
    [SerializeField]
    protected bool _isActive = false;

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

}
