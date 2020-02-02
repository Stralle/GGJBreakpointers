using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    CharacterMovement _character = null;
    bool _isSecondPhaseActive = false;
    [SerializeField]
    float _followSmoothTime = 0.5f;
    [SerializeField]
    float _moveToKnightSmoothTime = 0.1f;
    Vector3 _velocity = Vector3.zero; //Should be zero, always
    bool _shouldTranslateToKnight = false;
    Transform _enemyLocation = null;

    private void Start()
    {
        //I hate doing this but it's easier then drag-and-dropping the character to the placeholder for each level so just let it
        _character = FindObjectOfType<CharacterMovement>();
        if (_character == null)
        {
            Debug.LogError("Character is missing!");
            return;
        }

        GameRulesManager gameManager = GameRulesManager.Instance;
        gameManager.OnKnightSpawned += OnKnightSpawn;
        gameManager.OnOneSecondLeft += OnOneSecondLeft;
    }

    private void OnKnightSpawn(EnemyBase enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("The OnKnightSpawn is called but the enemy is null!");
            return;
        }
        _enemyLocation = enemy.transform;
        _shouldTranslateToKnight = true;
    }

    private void OnOneSecondLeft()
    {
        _isSecondPhaseActive = true;
    }

    private void Update()
    {
        Transform target;
        Vector3 targetPosition;
        if (!_isSecondPhaseActive)
        {
            target = _character.transform;
            targetPosition = new Vector3(target.position.x, target.position.y, -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _followSmoothTime);
        }

        if (_shouldTranslateToKnight)
        {
            target = _enemyLocation.transform;
            targetPosition = new Vector3(target.position.x, target.position.y, -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _moveToKnightSmoothTime);

            if (Input.GetMouseButtonDown(0))
            {
                _shouldTranslateToKnight = false;
            }
        }
    }
}
