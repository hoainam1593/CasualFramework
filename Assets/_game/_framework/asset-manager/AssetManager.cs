
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class AssetManager : SingletonMonoBehaviour<AssetManager>
{
	private AssetGroup assetGroup = new AssetGroup();

	protected override void OnDestroy()
	{
		base.OnDestroy();

		assetGroup.ReleaseGroup();
	}

	public AsyncOperationHandle<GameObject> LoadPrefab(string parentPath, string assetName)
	{
		return assetGroup.LoadPrefab(parentPath, assetName);
	}

	public AsyncOperationHandle<Sprite> LoadSprite(string parentPath, string assetName)
	{
		return assetGroup.LoadSprite(parentPath, assetName);
	}

	public AsyncOperationHandle<TextAsset> LoadText(string parentPath, string assetName)
	{
		return assetGroup.LoadText(parentPath, assetName);
	}

#if USE_SPINE
	public AsyncOperationHandle<SkeletonDataAsset> LoadSkeleton(string parentPath, string assetName)
	{
		return assetGroup.LoadSkeleton(parentPath, assetName);
	}
#endif
}