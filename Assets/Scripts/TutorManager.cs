using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Video;

public class TutorManager : MonoBehaviour
{
	VideoPlayer vp = null;

	// Start is called before the first frame update
	void Start()
    {
		vp = GetComponent<VideoPlayer>();
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu);
		}

		//if (!vp.isPlaying)
		//{
		//	SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu);
		//}
	}
}
