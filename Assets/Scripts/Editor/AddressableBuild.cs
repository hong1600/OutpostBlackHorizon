#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

public static class AddressablesAutoBuilder
{
    [MenuItem("Tools/Build Addressables")]
    public static void Build()
    {
        AddressableAssetSettings.BuildPlayerContent();
        UnityEngine.Debug.Log("Addressables ºôµå ¿Ï·á");
    }
}
#endif