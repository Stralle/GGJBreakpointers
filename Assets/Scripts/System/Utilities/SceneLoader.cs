using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{	
	public enum Scenes
	{
		MainMenu,
		GameLevel1,
		GameLevel2,
		Results,
		Tutorial
	}

	static public void LoadScene(Scenes scene)
	{
		Debug.Log("SceneLoader::LoadScene - " + scene.ToString());

		switch (scene)
		{
			case Scenes.MainMenu:
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
				break;
			case Scenes.GameLevel1:
				SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
				break;
			case Scenes.GameLevel2:
				SceneManager.LoadScene("Level1_1", LoadSceneMode.Single);
				break;
			case Scenes.Results:
				SceneManager.LoadScene("Results", LoadSceneMode.Single);
				break;
			case Scenes.Tutorial:
				SceneManager.LoadScene("OpeningScene", LoadSceneMode.Single);
				break;
			default:
				Debug.Assert(false);
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
				break;
		};
	}
}