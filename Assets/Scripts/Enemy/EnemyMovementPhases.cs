using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPhases : MonoBehaviour
{
	[System.Serializable]
	struct EnemyDecisionPoint
	{
		public Transform[] points;
	}

	//Random movement
	[SerializeField]
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

	// Start is called before the first frame update
	void Start()
    {
		_movementPhases = _movementPoints.Length;
		Debug.Assert(_movementPhases > 0, "Missing Movement Points!");
	}

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
		}
	}
}
