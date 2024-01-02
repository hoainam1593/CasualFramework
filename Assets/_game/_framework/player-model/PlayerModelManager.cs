
using System;
using System.Collections.Generic;

public class PlayerModelManager : SingletonMonoBehaviour<PlayerModelManager>
{
	private List<BasePlayerModel> lModels = new List<BasePlayerModel>()
	{
		//new models here
	};

	private IPlayerModelFile modelFile =
#if (UNITY_EDITOR && !EDITOR_USE_BINARY_MODEL) || (!UNITY_EDITOR && UNITY_STANDALONE)
		new PlayerModelFile_json();
#else
		new PlayerModelFile_binary();
#endif

	public T GetPlayerModel<T>() where T : BasePlayerModel
	{
		T result = null;
		var typeT = typeof(T);
		foreach (var i in lModels)
		{
			if (i.GetType() == typeT)
			{
				if (result == null)
				{
					result = (T)i;
				}
				else
				{
					throw new Exception($"there're more than 1 {typeT.Name} in lModels");
				}
			}
		}
		if (result != null)
		{
			return result;
		}
		else
		{
			throw new Exception($"there's no {typeT.Name} in lModels");
		}
	}

	public void LoadAllModels()
	{
		foreach (var i in lModels)
		{
			var path = GetModelFilePath(i);
			if (StaticUtils.CheckFileExist(path))
			{
				modelFile.ReadModel(path, i);
			}
			else
			{
				i.OnModelInitializing();
				WriteModel(i);
			}
			i.OnModelLoaded();
		}
	}

	public void WriteModel(BasePlayerModel model)
	{
		var path = GetModelFilePath(model);
		modelFile.WriteModel(path, model);
	}

	private string GetModelFilePath(BasePlayerModel model)
	{
		var parent =
#if UNITY_EDITOR
			"../";
#elif UNITY_STANDALONE
			"../../";
#else
			"";
#endif
		var folder = $"{parent}PlayerModels";
		var filename = $"{model.GetType().Name}.{modelFile.Extension}";
		return $"{folder}/{filename}";
	}
}