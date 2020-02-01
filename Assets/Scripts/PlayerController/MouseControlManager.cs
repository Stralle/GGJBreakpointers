using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JamGame
{
	public class MouseControlManager : Singleton<MouseControlManager>
	{
		Camera _camera = null;

		// add subscription for event if camera changes
		public Camera GetActiveCamera()
		{
			return _camera;
		}

		bool IsGameOnPause()
		{
			// hack: change to use some manager bool value (GameSceneManager)
			return Time.timeScale < 0.01f;
		}

		// Update is called once per frame
		void Update()
		{
			if (IsGameOnPause())
			{
				return;
			}

			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					Debug.Log("MouseControlManager: input provided, hit target: " + hit.collider.name);

					GameObject interactable = hit.collider.gameObject;
					if (interactable != null)
					{
						IDestructible destructible = interactable.GetComponent<Loot>() as IDestructible;
						if (destructible != null)
						{
							destructible.DestroyAndGetResources();
						}
					}
				}
			}
		}

		override protected void AwakeInitialization()
		{
			_camera = Camera.main;
		}
	}
}