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
				Debug.Log("MouseControlManager: mouse pressed");

				RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

				if (hit.collider != null)
				{
					Debug.Log("MouseControlManager: hit target: " + hit.collider.name);

					InteractablePart interactable = hit.collider.gameObject.GetComponent<InteractablePart>();
					Trap trap = hit.collider.gameObject.GetComponent<Trap>();

					if (interactable != null)
					{
						IDestructible destructible = interactable.GetMainGameObject().GetComponent<Loot>() as IDestructible;
						if (destructible != null)
						{
							destructible.DestroyAndGetResources();
						}
					}
					else if (trap != null)
					{
						trap.PlayerInteract();
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