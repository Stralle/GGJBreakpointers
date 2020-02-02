using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator _animator = null;

    private bool isFacingRight = true;

    public void SetMovementInputValue(Vector2 movementInput)
    {
        bool wasMoving = _animator.GetBool("IsMoving");
        bool isMoving = movementInput.magnitude > 0;

        if (movementInput.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1, 1);
            if (isFacingRight)
            {
                _animator.SetTrigger("TriggerTurn");
            }
            isFacingRight = false;
        }
        else if (movementInput.x > 0)
        {
            transform.localScale = new Vector3(1f, 1, 1);
            if (!isFacingRight)
            {
                _animator.SetTrigger("TriggerTurn");
            }
            isFacingRight = true;
        }

        _animator.SetBool("IsMoving", isMoving);
    }

    public void OnOneSecondLeft()
    {
        _animator.SetTrigger("TriggerEscape");
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator, "CharacterAnimator can't find animator!");
    }
}