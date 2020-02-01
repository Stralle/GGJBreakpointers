using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class IngameView : ViewBase
{
	[SerializeField] TextMeshProUGUI _firstPhaseTimer = null;

	bool _shouldShowTimer = true;

	private void Update()
	{
		if (_firstPhaseTimer && _shouldShowTimer)
		{
			int time = (int)GameRulesManager.Instance.Timer;
			_firstPhaseTimer.text = time.ToString();

			_shouldShowTimer = GameRulesManager.Instance.GamePhase == EGamePhase.Repare;
		}
	}
}
