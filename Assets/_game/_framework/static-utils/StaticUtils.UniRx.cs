using System;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public static partial class StaticUtils
{
	#region UI subscribe

	public static IDisposable SubscribeToFillAmount(this IObservable<float> observable, Image image)
	{
		return observable.Subscribe(value => image.fillAmount = value);
	}

	public static IDisposable SubscribeToText(this IObservable<string> observable, TextMeshProUGUI text)
	{
		return observable.Subscribe(value => text.text = value);
	}

	public static IDisposable SubscribeToActive(this IObservable<bool> observable, GameObject gameObject)
	{
		return observable.Subscribe(value => gameObject.SetActive(value));
	}

	#endregion

	#region rx collection

	public static bool Exists<T>(this ReactiveCollection<T> arr, Predicate<T> match)
	{
		foreach (var i in arr)
		{
			if (match.Invoke(i))
			{
				return true;
			}
		}
		return false;
	}

	public static T Find<T>(this ReactiveCollection<T> arr, Predicate<T> match) where T : class
	{
		foreach (var i in arr)
		{
			if (match.Invoke(i))
			{
				return i;
			}
		}
		return null;
	}

	public static int RemoveAll<T>(this ReactiveCollection<T> arr, Predicate<T> match)
	{
		var countRemoved = 0;
		for (var i = arr.Count - 1; i >= 0; i--)
		{
			if (match.Invoke(arr[i]))
			{
				arr.RemoveAt(i);
				countRemoved++;
			}
		}
		return countRemoved;
	}

	#endregion
}