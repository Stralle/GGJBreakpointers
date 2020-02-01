using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseControlManager : Singleton<MouseControlManager>
{
	Camera _camera = null;

	Vector3 moveStartPosition = Vector3.zero;
	[SerializeField]
	float _cameraMoveSpeed = 5.0f;
	float groundZ = 0;

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

		//-----------  Camera movement logic
		//The camera is moving with the right mouse button
		if (Input.GetMouseButtonDown(1))
		{
			moveStartPosition = GetWorldPosition(groundZ);
		}
		if (Input.GetMouseButton(1))
		{
			Camera activeCamera = GetActiveCamera();
			Vector3 direction = moveStartPosition - GetWorldPosition(groundZ);
			activeCamera.transform.position += direction;
		}

		if (GameRulesManager.Instance.GamePhase == EGamePhase.Defend)
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

	private Vector3 GetWorldPosition(float z)
	{
		Camera activeCamera = GetActiveCamera();
		Ray mousePos = activeCamera.ScreenPointToRay(Input.mousePosition);
		Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
		float distance;
		ground.Raycast(mousePos, out distance);
		return mousePos.GetPoint(distance);
	}

	override protected void AwakeInitialization()
	{
		_camera = Camera.main;
	}
}
