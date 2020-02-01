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
    ETrapType _trapType = ETrapType.Invalid;

    [SerializeField] 
    Animator _animator = null;

    [SerializeField]
    protected bool _isRepaired = false;
    [SerializeField]
    protected bool _isActive = false;

    public virtual ETrapType GetTrapType()
    {
        return _trapType;
    }

    public virtual void RepairAndSpendResources()
    {
        // TODO: Implement logic here and stuff
        throw new System.NotImplementedException();
    }
}
