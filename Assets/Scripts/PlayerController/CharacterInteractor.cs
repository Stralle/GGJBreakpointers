using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    bool _isInteractingWithTrap = false;
    [SerializeField]
    Trap _selectedTrap = null;
    Animator _animator = null;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        Debug.Assert(_animator, "Can't find animator!");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        InteractablePart _resource = collision.GetComponent<InteractablePart>();
        Trap _trap = collision.gameObject.GetComponent<Trap>();
        if (_resource)
        {
            IDestructible destructible = _resource.GetMainGameObject().GetComponent<Loot>() as IDestructible;
            if (destructible != null)
            {
                if(destructible.DestroyAndGetResources())
                {
                    _animator.SetTrigger("TriggerSmash");
                }
            }
        }
        if (_trap != null)
        {
            _isInteractingWithTrap = true;
            //Select the closest trap
            if (_selectedTrap)
            {
                float distanceToCurrentTrap = Vector2.Distance(_selectedTrap.transform.position, gameObject.transform.position);
                float distanceToNewTrap = Vector2.Distance(_trap.transform.position, gameObject.transform.position);
                if (distanceToNewTrap < distanceToCurrentTrap)
                {
                    //The new trap is the closest one now, select it and deselect the old one
                    DeselectTrap(_selectedTrap);
                    _selectedTrap = _trap;
                }
            }
            else
            {
                _selectedTrap = _trap;
            }

            _trap.SetUIFeedbackActive(true);
        }
    }

    private void DeselectTrap(Trap trap)
    {
        _selectedTrap = null;
        trap.SetUIFeedbackActive(false);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Trap _trap = collision.gameObject.GetComponent<Trap>();
        if (_trap)
        {
            DeselectTrap(_trap);
        }
    }

    private void Update()
    {
        if (_selectedTrap)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(_selectedTrap.PlayerInteract())
                {
                    _animator.SetTrigger("TriggerSmash");
                }
            }
        }
    }
}
