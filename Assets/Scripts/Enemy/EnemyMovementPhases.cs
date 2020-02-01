using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[System.Serializable]
	public class EnemyDecisionPoint
	{
		public Transform[] points;
	}

public class EnemyMovementPhases : MonoBehaviour
{
	EnemyDecisionPoint[] _movementPoints = new EnemyDecisionPoint[0];
	int _movementPhases = 0;
	int _currentMovementPhase = -1;
	Transform _selectedPoint = null;
	bool _isMovementFinished = false;
	bool _isMoving = false;
	[SerializeField]
	float _distanceTolerance = 0;
	[SerializeField] 
	float _movementSpeed = 5f;

	//Components
	SpriteRenderer _spriteRenderer = null;

    // Update is called once per frame
    void Update()
    {
		UpdateMovement();
    }

    private void UpdateMovement()
    {
		if (_isMoving)
		{
			if (_selectedPoint == null)
			{
				Debug.LogError("No selection point.");
				return;
			}
			transform.position = Vector2.MoveTowards(transform.position, _selectedPoint.position, _movementSpeed * Time.deltaTime);

			float _distanceToPoint = Vector2.Distance(transform.position, _selectedPoint.position);
			if (_distanceToPoint - _distanceTolerance < 0)
			{
				_isMoving = false;
				Debug.Log("Point Reached. Selecting the next one.");
			}
		}
		else
		{
			++_currentMovementPhase;
			if (_currentMovementPhase > _movementPhases - 1)
			{
				return;
			}
			int _randomPoint = UnityEngine.Random.Range(0, _movementPoints[_currentMovementPhase].points.Length);
			_selectedPoint = _movementPoints[_currentMovementPhase].points[_randomPoint];
			_isMoving = true;

			//Update moving direction
			CalculateMovingDirection();
			
		}
	}

	void CalculateMovingDirection()
	{
		if (_selectedPoint == null)
		{
			Debug.LogError("Selected Point is null!");
			return;
		}

		if (_selectedPoint.position.x < transform.position.x)
		{
			_spriteRenderer.flipX = true;
		}
		else
		{
			_spriteRenderer.flipX = false;
		}
	}

	public void SetMovementPoints(EnemyDecisionPoint[] _decisionPoints)
	{
		_movementPoints = _decisionPoints;
		Init();
	}

	private void Init()
	{
		_movementPhases = _movementPoints.Length;
		Debug.Assert(_movementPhases > 0, "Missing Movement Points!");
		_spriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Assert(_spriteRenderer, "Missing the Sprite Renderer Component!");
	}
}
