using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResultsSceneManager : MonoBehaviour
{
	[SerializeField] ResultsView _resultsView = null;

	private void Start()
	{
		Debug.Assert(_resultsView);
		_resultsView.Show();
	}

	public void RestartGame()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		SceneLoader.LoadScene(SceneLoader.Scenes.Game);
	}

	public void ExitToMenu()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu);
	}
}
