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
    [SerializeField] ETrapType _trapType = ETrapType.Invalid;

    [SerializeField] Animator _animator = null;

    bool _isRepaired = false;

    bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ETrapType GetTrapType()
    {
        return _trapType;
    }

    public void RepairAndSpendResources()
    {
        // TODO: Implement logic here and stuff
        throw new System.NotImplementedException();
    }
}
