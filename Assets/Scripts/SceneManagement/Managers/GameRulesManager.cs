using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGamePhase
{
	Repare,
	Defend
}

public class GameRulesManager : Singleton<GameRulesManager>, IGameRulesManager
{
	private EGamePhase _gamePhase = EGamePhase.Repare;
	public EGamePhase GamePhase => _gamePhase;

	float _timer = 0f;
	public float Timer => _timer;
	public float TIME_OF_REPARE_PHASE = 30f;

	// subscription for starting second phase
	public event Action OnStartSecondPhase = delegate {};
	//Triggers when the time goes bellow 1 second
	public event Action OnOneSecondLeft = delegate {};
	//Event called when the knight is spawned
	public event Action<EnemyBase> OnKnightSpawned = delegate { };

	int[] _itemsCollected;

	private void Start()
	{
		_gamePhase = EGamePhase.Repare;

		_timer = TIME_OF_REPARE_PHASE;

		Debug.Assert(!_doNotDestroyOnLoad, "Manager should be destroyed on Load");
		_itemsCollected = new int[(int)EResourceType.Size];
		for (int i = 0; i < (int)EResourceType.Size; i++)
		{
			_itemsCollected[i] = 0;
		}
	}

	private void Update()
	{
		if (GamePhase == EGamePhase.Repare)
		{
			_timer -= Time.deltaTime;
			if (_timer > 0 && _timer < 1.0f)
			{
				OnOneSecondLeft.Invoke();
			}
			else if (_timer < 0)
			{
				GoToTheSecondStage();
			}
		}
	}

	public int GetAmountOfResources(EResourceType type)
	{
		return _itemsCollected[(int)type];
	}

	public void ResourcesCollected(EResourceType type, int amount)
	{
		_itemsCollected[(int)type] += amount;
	}

    public void ResourcesSpent(EResourceType type, int amount)
    {
        if (_itemsCollected[(int)type] >= amount)
        {
            _itemsCollected[(int)type] -= amount;
        }
        Debug.Log("Not enough resources of type " + type.ToString());
    }

	// place to run the whole logic of switching phase
	void GoToTheSecondStage()
	{
		_gamePhase = EGamePhase.Defend;

		OnStartSecondPhase.Invoke();
	}

	public virtual void EndGame()
	{
		// todo:: add save of the results
		SceneLoader.LoadScene(SceneLoader.Scenes.Results);
	}

	public virtual void ReturnToMenu()
	{
		SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu);
	}

	public void OnEnemySpawned(EnemyBase enemy)
	{
		OnKnightSpawned.Invoke(enemy);
	}
}
