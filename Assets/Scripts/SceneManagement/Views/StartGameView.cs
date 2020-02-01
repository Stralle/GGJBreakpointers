using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameView : ViewBase
{
	[SerializeField] Button _exit = null;

	public override void UpdateComponents()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer
			|| Application.platform == RuntimePlatform.OSXPlayer)
		{
			_exit.gameObject.SetActive(true);
		}
		else
		{
			_exit.gameObject.SetActive(false);
		}
	}
}
