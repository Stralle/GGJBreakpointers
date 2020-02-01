using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
	[SerializeField] PauseView _pauseView = null;
	[SerializeField] IngameView _ingameView = null;
	GameRulesManager _gameRulesManager = null;

	private void Start()
	{
		_gameRulesManager = FindObjectOfType<GameRulesManager>();
		Debug.Assert(_gameRulesManager != null);
		Debug.Assert(_pauseView && _ingameView);

		_pauseView.Hide();
		_ingameView.Show();

		//SoundManager.Instance.StartGameMainTheme();
	}

	public void PauseGame()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		Time.timeScale = 0.0f;
		_pauseView.Show();
		_ingameView.Hide();
	}

	public void ResumeGame()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		Time.timeScale = 1.0f;
		_pauseView.Hide();
		_ingameView.Show();
	}

	public void ExitToResults()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		Time.timeScale = 1.0f;
		_gameRulesManager.EndGame();
	}

	public void ExitToMenu()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		Time.timeScale = 1.0f;
		_gameRulesManager.ReturnToMenu();
	}
}
