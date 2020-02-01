using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo : GenericSingletonClass<PlayerInfo>
{
	private bool _initialized = false;

	private string _dataPath;
	private PlayerData _playerData;

	public PlayerData PlayerData => _playerData;

	public int GetPlayerMaxScores() => _playerData.maxScores;
	public void SetPlayerMaxScores(int scores)
	{
		_playerData.maxScores = scores;
	}

	public string GetPlayerName() => _playerData.playerName;
	public void SetPlayerName(string name)
	{
		_playerData.playerName = name;
	}

	public void SaveData()
	{
		Debug.Assert(_initialized);

		string curTime = DateTime.Now.ToString();
		_playerData.lastSevedTime = curTime;

		JsonSaveSystem.SavePlayer(_playerData, _dataPath);
	}

	public void LoadData()
	{
		Debug.Assert(_initialized);
		_playerData = JsonSaveSystem.LoadPlayer(_dataPath);
	}

	// LazyInitialized implementation
	public override bool IsInitialized()
	{
		return _initialized;
	}

	public override void Initialize() 
	{
		Debug.Log("PlayerInfo::Initialize");

		// TODO: add consts loading (scriptable objects)

		_dataPath = Path.Combine(Application.persistentDataPath, "PlayerInfo.txt");
		_initialized = true;

		LoadData();
	}
}
