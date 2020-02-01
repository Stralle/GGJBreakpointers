using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class SettingsView : ViewBase
{
	[SerializeField] InputField _playerName_input = null;
	[SerializeField] TextMeshProUGUI _playerName_value = null;

	[SerializeField] Button _soundButton = null;
	[SerializeField] TextMeshProUGUI _soundButtonText = null;

	bool _haveSomethingToSave = false;

	private void Start()
	{
		if (_playerName_input)
		{
			_playerName_input.onEndEdit.AddListener(SubmitName);
		}

		if (_soundButton)
		{
			_soundButton.onClick.AddListener(() => ToggleSound());
		}
	}

	private void SubmitName(string arg0)
	{
		Debug.Log("SettingsView:SubmitName - " + arg0);
		_haveSomethingToSave = true;
	}

	public void ApplySettings()
	{
		if (_haveSomethingToSave)
		{
			Debug.Log("SettingsView:ApplySettings");

			PlayerInfo.Instance.SetPlayerName(_playerName_input.text);
			PlayerInfo.Instance.SaveData();

			_haveSomethingToSave = false;
		}
	}

	public override void Show()
	{
		base.Show();
		_haveSomethingToSave = false;
	}

	void ToggleSound()
	{
		SoundManager.Instance.SetMute(!SoundManager.Instance.Mute);
		UpdateComponents();

		if (!SoundManager.Instance.Mute)
		{
			SoundManager.Instance.StartGameMainTheme();
		}
	}

	public override void UpdateComponents()
	{
		if (_soundButton)
		{
			bool isMuted = SoundManager.Instance.Mute;
			string soundText = "Sound OFF";
			if (isMuted)
			{
				soundText = "Sound ON";
			}
			_soundButtonText.text = soundText;
		}

		if (_playerName_value != null)
		{
			string player_name = PlayerInfo.Instance.GetPlayerName();
			if (player_name.Length == 0)
			{
				player_name = "NoName";
			}
			_playerName_value.text = player_name;

			_playerName_input.text = player_name;
		}
	}
}
