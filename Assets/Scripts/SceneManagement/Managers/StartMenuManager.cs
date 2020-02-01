using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
	[SerializeField] StartGameView _mainView = null;
	[SerializeField] SettingsView _settingsView = null;

	private void Start()
	{
		Debug.Assert(_mainView && _settingsView);

		_settingsView.Hide();
		_mainView.Show();

		//SoundManager.Instance.StartGameMainTheme();
	}

	// MainView interaction
	public void OpenSettings()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		_mainView.Hide();
		_settingsView.Show();
	}

	public void OnExitPressed()
	{
		Application.Quit();
	}


	public void StartGame()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		SceneLoader.LoadScene(SceneLoader.Scenes.Game);
	}

	// SettingsView interaction
	public void CloseSettings()
	{
		//SoundManager.Instance.PlayButtonPressedSound();
		_settingsView.Hide();
		_mainView.Show();
	}
}