using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitAnimation : MonoBehaviour
{
	public void OnButtonPressed()
	{
		GetComponent<Animation>().Play("ButtonExitGame");
	}
}
