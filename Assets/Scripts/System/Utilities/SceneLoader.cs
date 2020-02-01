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
				SceneManager.LoadScene("StartGame", LoadSceneMode.Single);
				break;
			case Scenes.Game:
				SceneManager.LoadScene("SampleScene01", LoadSceneMode.Single);
				break;
			case Scenes.Results:
				SceneManager.LoadScene("ResultsScene", LoadSceneMode.Single);
				break;
			default:
				Debug.Assert(false);
				SceneManager.LoadScene("StartGame", LoadSceneMode.Single);
				break;
		};
	}
}