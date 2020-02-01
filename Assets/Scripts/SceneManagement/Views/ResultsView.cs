using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ResultsView : ViewBase
{
	[SerializeField] TextMeshProUGUI _playerScores_value = null;
	[SerializeField] TextMeshProUGUI _playerName = null;

	public override void UpdateComponents()
	{
		if (_playerScores_value != null)
		{
			_playerScores_value.text = PlayerInfo.Instance.GetPlayerMaxScores().ToString();
		}

		if (_playerName)
		{
			_playerName.text = PlayerInfo.Instance.GetPlayerName();
		}

		PlayerInfo pInfo = PlayerInfo.Instance;
	}
}
