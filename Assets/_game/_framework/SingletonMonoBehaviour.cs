
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
	public static T instance { get; private set; }

	protected virtual void Awake()
	{
		instance = (T)this;
	}

	protected virtual void OnDestroy()
	{
		instance = null;
	}
}