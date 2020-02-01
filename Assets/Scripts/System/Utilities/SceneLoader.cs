using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{	
	public enum Scenes
	{
		MainMenu,
		Game,
		Results
	}

	static public void LoadScene(Scenes scene)
	{
		Debug.Log("SceneLoader::LoadScene - " + scene.ToString());

		switch (scene)
		{
			case Scenes.MainMenu:
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
				break;
			case Scenes.Game:
				SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
				break;
			case Scenes.Results:
				SceneManager.LoadScene("Results", LoadSceneMode.Single);
				break;
			default:
				Debug.Assert(false);
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
				break;
		};
	}
}