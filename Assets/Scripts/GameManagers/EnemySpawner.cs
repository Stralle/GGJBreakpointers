using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] EnemyBase _enemyPrefab = null;
	[SerializeField] Transform _positionToSpawn;

	[SerializeField] EnemyDecisionPoint[] _movementPoints = new EnemyDecisionPoint[0];

	public void SpawnEnemy()
	{
		EnemyBase enemy = Instantiate(_enemyPrefab);
		enemy.SetInitialPosition(new Vector2(_positionToSpawn.position.x, _positionToSpawn.position.y));
		enemy.SetMovementPath(_movementPoints);
	}

	private void Start()
	{
	}
}
