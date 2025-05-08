using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bundle : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ILoad());
    }

    IEnumerator ILoad()
    {
        AssetBundleCreateRequest async =
            AssetBundle.LoadFromFileAsync(Path.Combine
            (Application.streamingAssetsPath, "monster"));

        yield return async;

        AssetBundle local = async.assetBundle;

        if(local == null)
            yield break;

        AssetBundleRequest asset = local.LoadAllAssetsAsync<GameObject>();

        yield return asset;

        var prefab = asset.asset as GameObject;

        local.Unload(true);
    }
}
