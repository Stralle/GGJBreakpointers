using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// show view with loose/win text for some time
public class WinLooseTextView : MonoBehaviour
{
	[Range(0, 10)]
	[SerializeField] float TIME_TO_SHOW_TEXT = 3f;

	[SerializeField] GameObject _winText = null;
	[SerializeField] GameObject _looseText = null;

	[SerializeField] AudioClip _onWinSound = null;
	[SerializeField] AudioClip _onLooseSound = null;

	float _timer = -1f;

	void Start()
	{
		GameRulesManager.Instance.OnLevelFinished += OnLevelEnd;

		_winText.SetActive(false);
		_looseText.SetActive(false);
	}

	void OnLevelEnd(bool playerWon)
	{
		if (playerWon)
		{
			if (_onWinSound)
			{
				SoundManager.Instance.PlayOneTimeSound(_onWinSound, 0.4f);
			}
			_winText.SetActive(true);
		}
		else
		{
			if (_onLooseSound)
			{
				SoundManager.Instance.PlayOneTimeSound(_onLooseSound, 0.4f);
			}
			_looseText.SetActive(true);
		}

		_timer = TIME_TO_SHOW_TEXT;

		if (_timer <= 0f)
		{
			GameRulesManager.Instance.ResultsTextWasShown();
		}
	}

	void Update()
	{
		if (_timer > 0f)
		{
			_timer -= Time.deltaTime;

			if (_timer <= 0f)
			{
				GameRulesManager.Instance.ResultsTextWasShown();
			}
		}
	}
}
