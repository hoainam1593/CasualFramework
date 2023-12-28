using UnityEngine;

public static partial class StaticUtils
{
	public static bool IsBeginTouchScreen()
	{
		return Input.GetMouseButtonDown((int)MouseButtonType.LeftMouse);
	}

	public static Vector3 GetTouchPosition()
	{
		return Input.mousePosition;
	}

	public static bool IsPressBackKey()
	{
		return Input.GetKeyDown(KeyCode.Escape);
	}
}