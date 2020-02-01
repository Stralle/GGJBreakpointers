using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBase : MonoBehaviour
{
	public virtual void UpdateComponents()
	{
	}

	public virtual void Show()
	{
		UpdateComponents();
		gameObject.SetActive(true);
	}

	public virtual void Hide()
	{
		gameObject.SetActive(false);
	}
}
