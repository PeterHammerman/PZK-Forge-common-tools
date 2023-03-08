using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePermaInstance<T> : MonoBehaviour where T : Component
{


	public static bool HasInstance => _instance != null;
	public static T Current => _instance;

	protected static T _instance;
	protected bool _enabled;

//Instance
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
				if (_instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name + "_Instance";
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	/// check if there is another instance in scene, then destroy it

	protected virtual void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}

			this.transform.SetParent(null);

		if (_instance == null)
		{
			//If this is first gameobject, make it undestroyable and fill variable of instance
			_instance = this as T;
			DontDestroyOnLoad(transform.gameObject);
			_enabled = true;
		}
		else
		{
			//Destroy
			if (this != _instance)
			{
				Destroy(this.gameObject);
			}
		}
	}
}

