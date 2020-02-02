using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    CharacterMovement _character = null;
    bool _isSecondPhaseActive = false;
    [SerializeField]
    float _followSmoothTime = 0.5f;
    Vector3 _velocity = Vector3.zero; //Should be zero, always

    private void Start()
    {
        //I hate doing this but it's easier then drag-and-dropping the character to the placeholder for each level so just let it
        _character = FindObjectOfType<CharacterMovement>();
        if (_character == null)
        {
            Debug.LogError("Character is missing!");
            return;
        }

        GameRulesManager.Instance.OnStartSecondPhase += OnSecondPhaseStarted;
    }

    private void OnSecondPhaseStarted()
    {
        _isSecondPhaseActive = true;
    }

    private void Update()
    {
        if (!_isSecondPhaseActive)
        {
            Vector3 targetPosition = new Vector3(_character.transform.position.x, _character.transform.position.y, -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _followSmoothTime);
        }
    }
}
