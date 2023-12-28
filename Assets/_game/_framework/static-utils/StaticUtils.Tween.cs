using DG.Tweening;
using UnityEngine;

public static partial class StaticUtils
{
	public static void ScaleGameObject(Transform target, float startScale, float endScale, float timeScale,
		Ease ease, TweenCallback doneCallback)
	{
		target.localScale = Vector3.one * startScale;
		target.DOScale(endScale, timeScale).SetEase(ease).OnComplete(doneCallback);
	}

	public static void MoveUIObject(RectTransform target, Vector2 startPos, Vector2 endPos, float timeMove,
		Ease ease, TweenCallback doneCallback)
	{
		target.anchoredPosition = startPos;
		target.DOAnchorPos(endPos, timeMove).SetEase(ease).OnComplete(doneCallback);
	}
}