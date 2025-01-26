using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BundleEditor : MonoBehaviour
{
    [MenuItem("Assets/AssetBundles")]
    static void BuildAssetBundles()
    {
        string dir = "Assets/StreamingAssets";

        if(!Directory.Exists(Application.streamingAssetsPath)) 
        {
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
    }
}
