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

	int[] _itemsCollected;

	private void Start()
	{
		_gamePhase = EGamePhase.Repare;

		_timer = TIME_OF_REPARE_PHASE;

		Debug.Assert(_doNotDestroyOnLoad, "Manager should be destroyed on Load");
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
			if (_timer < 0)
			{
				GoToTheSecondStage();
			}
		}
	}

	public void ResourcesCollected(EResourceType type, int amount)
	{
		_itemsCollected[(int)type] += amount;
	}

    public void ResourcesSpent(EResourceType type, int amount)
    {
        _itemsCollected[(int)type] -= amount;
    }

	// place to run the whole logic of switching phase
	void GoToTheSecondStage()
	{
		_gamePhase = EGamePhase.Defend;
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
}
