using System.IO;
using UnityEngine;

public class JsonSaveSystem
{
	#region PlayerInfo
	public static void SavePlayer(PlayerData data, string path)
	{
		Debug.Log("JsonSaveSystem::SavePlayer");

		string jsonString = JsonUtility.ToJson(data);
		Debug.Log("JsonSaveSystem::SavePlayer: jsonString - " + jsonString);

		using (StreamWriter streamWriter = File.CreateText(path))
		{
			streamWriter.Write(jsonString);
		}
	}

	public static PlayerData LoadPlayer(string path)
	{
		Debug.Log("JsonSaveSystem::LoadPlayer");

		if (!File.Exists(path))
		{
			Debug.Log("JsonSaveSystem::LoadPlayer - file does not exist, path: " + path);
			return new PlayerData();
		}

		using (StreamReader streamReader = File.OpenText(path))
		{
			string jsonString = streamReader.ReadToEnd();
			Debug.Log("JsonSaveSystem::LoadPlayer: jsonString - " + jsonString);

			return JsonUtility.FromJson<PlayerData>(jsonString);
		}
	}
	#endregion // PlayerInfo
}
