using System.Runtime.ExceptionServices;
using System.Text;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static partial class StaticUtils
{
	#region character

	public static bool IsDigitCharacter(char c)
	{
		return c >= '0' && c <= '9';
	}

	public static bool IsLowercaseAlphabetCharacter(char c)
	{
		return c >= 'a' && c <= 'z';
	}

	public static bool IsUppercaseAlphabetCharacter(char c)
	{
		return c >= 'A' && c <= 'Z';
	}

	public static bool IsAlphabetCharacter(char c)
	{
		return IsLowercaseAlphabetCharacter(c) || IsUppercaseAlphabetCharacter(c);
	}

	#endregion

	#region exception

	public static string AggregateExceptionToString(AggregateException aggE)
	{
		var flatE = aggE.Flatten();
		var sb = new StringBuilder();
		for (var i = 1; i <= flatE.InnerExceptions.Count; i++)
		{
			sb.Append($"exception {i}={flatE.InnerExceptions[i - 1].Message}");
			if (i < flatE.InnerExceptions.Count)
			{
				sb.Append(" ");
			}
		}
		return sb.ToString();
	}

	public static void RethrowException(Exception e)
	{
		ExceptionDispatchInfo.Capture(e).Throw();
	}

	#endregion

	#region json

	public static string JsonSerializeToFriendlyText(object obj)
	{
		var settings = new JsonSerializerSettings();
		settings.Formatting = Formatting.Indented;
		settings.Converters.Add(new StringEnumConverter());
		return JsonConvert.SerializeObject(obj, settings);
	}

	public static T CastJsonValue<T>(object jsonValue)
	{
		if (jsonValue is JObject)
		{
			var jo = (JObject)jsonValue;
			return jo.ToObject<T>();
		}
		else if (jsonValue is JArray)
		{
			var ja = (JArray)jsonValue;
			return ja.ToObject<T>();
		}
		else //string, number, boolean
		{
			return (T)jsonValue;
		}
	}

	public static string FormatJson(string json)
	{
		using (var stringReader = new StringReader(json))
		using (var stringWriter = new StringWriter())
		{
			var jsonReader = new JsonTextReader(stringReader);
			var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
			jsonWriter.WriteToken(jsonReader);
			return stringWriter.ToString();
		}
	}

	#endregion

	#region version

	//version convention: [major].[minor].[build]
	// - dev/test phase: [major], [minor] are always zero, just increase [build]
	// - live phase:
	//   + big update: increase [major]
	//   + minor update: increase [minor]
	//   + daily test/dev build: increase [build]

	public static string IncreaseVersion(string verStr, VersionComponent component = VersionComponent.Build)
	{
		var ver = new Version(verStr);
		var build = ver.Build;
		var minor = ver.Minor;
		var major = ver.Major;
		switch (component)
		{
			case VersionComponent.Build:
				build++;
				break;
			case VersionComponent.Minor:
				minor++;
				build = 0;
				break;
			case VersionComponent.Major:
				major++;
				minor = 0;
				build = 0;
				break;
		}
		return new Version(major, minor, build).ToString();
	}

	public static int CompareVersion(string strVer1, string strVer2)
	{
		return new Version(strVer1).CompareTo(new Version(strVer2));
	}

	#endregion

	#region other utils

	public static string RandomAnUID(int length,
		bool hasNumber = true, bool hasLowercase = true, bool hasUppercase = true)
	{
		var listChars = new List<char>();
		if (hasNumber)
		{
			for (var c = '0'; c <= '9'; c++)
			{
				listChars.Add(c);
			}
		}
		if (hasLowercase)
		{
			for (var c = 'a'; c <= 'z'; c++)
			{
				listChars.Add(c);
			}
		}
		if (hasUppercase)
		{
			for (var c = 'A'; c <= 'Z'; c++)
			{
				listChars.Add(c);
			}
		}

		var sb = new StringBuilder();
		for (var i = 0; i < length; i++)
		{
			var rNumber = UnityEngine.Random.Range(0, listChars.Count);
			sb.Append(listChars[rNumber]);
		}
		return sb.ToString();
	}

	public static string Xor(string str, string key)
	{
		var sb = new StringBuilder();
		for (int i = 0; i < str.Length; i++)
		{
			sb.Append((char)(str[i] ^ key[i % key.Length]));
		}
		return sb.ToString();
	}

	public static void CopyToClipboard(string txt)
	{
		GUIUtility.systemCopyBuffer = txt;
	}

	public static void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public static Texture2D CreateColorTexture(Color color)
	{
		var pix = new Color[1];
		pix[0] = color;

		var result = new Texture2D(1, 1);
		result.SetPixels(pix);
		result.Apply();

		return result;
	}

	public static bool IsAppRunning(string appName)
	{
		return Process.GetProcessesByName(appName).Length > 0;
	}

	public static PlatformType GetCurrentPlatform()
	{
#if UNITY_EDITOR
		return PlatformType.Editor;
#elif UNITY_STANDALONE
        return PlatformType.Standalone;
#elif UNITY_ANDROID
        return PlatformType.Android;
#elif UNITY_IOS
        return PlatformType.Ios;
#endif
	}

	public static async UniTask<string> PostHttpRequest(string url, string json)
	{
		var req = UnityWebRequest.Put(url, json);
		req.SetRequestHeader("Content-Type", "application/json");
		try
		{
			var op = await req.SendWebRequest();
			return op.downloadHandler.text;
		}
		catch (UnityWebRequestException e)
		{
			throw new HttpPostException(e);
		}
		finally
		{
			req.Dispose();
		}
	}

	//transaction: task A -> delay(time, cancelToken) -> task B
	//if we use UniTask.Delay, when cancel, it will cancel the whole transaction,
	//mean task B won't be executed
	public static async UniTask DelayNonBreak(float secondsDelay, CancellationToken cancellationToken)
	{
		var t = 0f;
		while (t < secondsDelay && !cancellationToken.IsCancellationRequested)
		{
			await UniTask.DelayFrame(1);
			t += Time.deltaTime;
		}
	}

	#endregion
}