using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	[SerializeField] AudioSource OneTimeAudioSourcePrefab = null;
	[SerializeField] AudioSource LoopAudioSourcePrefab = null;

	private AudioSource _menuLoopSource = null;
	private AudioSource _gameLoopSource = null;
	private List<AudioSource> _oneTimeSources = null;

	// game sounds
	[SerializeField]
	AudioClip GameThemeSound = null;
	[SerializeField]
	AudioClip MainMenuSound = null;

	[SerializeField]
	AudioClip ButtonPressSound = null;

	[SerializeField]
	int _numOfSimultaneousSources = 7;

	bool _mute = false;

	public bool Mute => _mute;

	protected override void AwakeInitialization()
	{
		base.AwakeInitialization();

		if (_oneTimeSources == null)
		{
			_oneTimeSources = new List<AudioSource>();
		}

		for (int i = 0; i < _numOfSimultaneousSources; i++)
		{
			AudioSource source = Instantiate<AudioSource>(OneTimeAudioSourcePrefab);
			source.transform.parent = gameObject.transform;
			_oneTimeSources.Add(source);
		}

		_menuLoopSource = Instantiate<AudioSource>(LoopAudioSourcePrefab);
		_menuLoopSource.transform.parent = gameObject.transform;

		_gameLoopSource = Instantiate<AudioSource>(LoopAudioSourcePrefab);
		_gameLoopSource.transform.parent = gameObject.transform;
	}

	public void SetMute(bool mute)
	{
		if (!_mute)
		{
			StopAllSounds();
		}

		_mute = mute;
	}

	public void StopAllSounds()
	{
		foreach (AudioSource source in _oneTimeSources)
		{
			source.Stop();
		}

		_menuLoopSource.Stop();
		_gameLoopSource.Stop();
	}

	public void StartMainMenuTheme(float volume = 0.05f)
	{
		if (_mute)
		{
			return;
		}

		_menuLoopSource.clip = MainMenuSound;
		_menuLoopSource.volume = volume;
		_menuLoopSource.Play();
	}

	public void StartGameMainTheme(float volume = 0.25f)
	{
		if (_mute)
		{
			return;
		}

		if (!_gameLoopSource.isPlaying)
		{
			_gameLoopSource.clip = GameThemeSound;
			_gameLoopSource.volume = volume;
			_gameLoopSource.Play();
		}
	}

	public void PlayButtonPressedSound()
	{
		PlayOneTimeSound(ButtonPressSound);
	}

	public void PlayOneTimeSound(AudioClip sound, float volume = 0.6f)
	{
		if (_mute)
		{
			return;
		}

		bool result = false;

		foreach (AudioSource source in _oneTimeSources)
		{
			if (!source.isPlaying)
			{
				source.clip = sound;

				source.volume = volume;
				source.Play();
				result = true;
				break;
			}
		}

		Debug.Assert(result, "Don't have enough audio sources");
	}
}
