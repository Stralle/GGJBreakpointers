using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// part of the object to make it interactable for player
public class InteractablePart : MonoBehaviour
{
	public GameObject GetMainGameObject()
	{
		// make it more complex later
		return transform.parent.gameObject;
	} 
}