using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LazyInitialized
{
	bool IsInitialized();
	void Initialize();
}

// explained here: http://www.unitygeek.com/unity_c_singleton/
public class GenericSingletonClass<T> : MonoBehaviour, LazyInitialized where T : Component, LazyInitialized
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}

			if (!instance.IsInitialized())
			{
				instance.Initialize();
			}

			return instance;
		}
	}

	public static bool CheckIfHaveInstanse() { return instance != null || FindObjectOfType<T>() != null; }

	// to make singleton work inside one scene
	[SerializeField] bool _singleton_setDoNotDestroyOnLoad = true;

	public virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			if (_singleton_setDoNotDestroyOnLoad)
			{
				DontDestroyOnLoad(this.gameObject);
			}

			AwakeInitialization();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// LazyInitialized implementation
	public virtual bool IsInitialized()
	{
		return true;
	}
	public virtual void Initialize() { }

	protected virtual void AwakeInitialization() { }
}