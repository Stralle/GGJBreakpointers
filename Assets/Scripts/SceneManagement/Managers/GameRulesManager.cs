using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRulesManager : MonoBehaviour, IGameRulesManager
{
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
