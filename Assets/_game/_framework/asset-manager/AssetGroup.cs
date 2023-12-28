#if USE_SPINE
using Spine.Unity;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetGroup
{
	private List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();

	private AsyncOperationHandle<T> LoadAsset<T>(string path)
	{
		var handle = Addressables.LoadAssetAsync<T>(path);
		handles.Add(handle);
		return handle;
	}

	public AsyncOperationHandle<GameObject> LoadPrefab(string parentPath, string assetName)
	{
		var fullPath = $"{parentPath}/{assetName}.prefab";
		return LoadAsset<GameObject>(fullPath);
	}

	public AsyncOperationHandle<Sprite> LoadSprite(string parentPath, string assetName)
	{
		var fullPath = $"{parentPath}/{assetName}.png";
		return LoadAsset<Sprite>(fullPath);
	}

	public AsyncOperationHandle<TextAsset> LoadText(string parentPath, string assetName)
	{
		var fullPath = $"{parentPath}/{assetName}.txt";
		return LoadAsset<TextAsset>(fullPath);
	}

#if USE_SPINE
	public AsyncOperationHandle<SkeletonDataAsset> LoadSkeleton(string parentPath, string assetName)
	{
		var fullPath = $"{parentPath}/{assetName}.asset";
		return LoadAsset<SkeletonDataAsset>(fullPath);
	}
#endif

	public void ReleaseGroup()
	{
		foreach (var handle in handles)
		{
			Addressables.Release(handle);
		}
		handles.Clear();
	}
}
