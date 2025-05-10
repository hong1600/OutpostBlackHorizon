using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SceneResourceBase<T> : MonoBehaviour where T : Enum
{
    [SerializeField] protected T[] typeEnums;

    protected Dictionary<T, GameObject> prefabDic = new Dictionary<T, GameObject>();

    protected void LoadSceneResources(T[] _adresses)
    {
        for (int i = 0; i < _adresses.Length; i++)
        {
            T type = _adresses[i];
            string address = ConvertEnumToAddress(_adresses[i]);

            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);
            RegisterCallback(type, handle);
        }
    }

    public abstract void LoadResources();

    private void RegisterCallback(T _type, AsyncOperationHandle<GameObject> _handle)
    {
        _handle.Completed += delegate (AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                prefabDic[_type] = handle.Result;
            }
        };
    }

    public virtual void UnloadSceneResources()
    {
        foreach (var pair in prefabDic)
        {
            Addressables.Release(pair.Value);
        }

        prefabDic.Clear();
    }

    public virtual GameObject GetPrefab(T _type)
    {
        if (prefabDic.TryGetValue(_type, out var obj))
        {
            return obj;
        }
        return null;
    }

    public virtual void addPrefabDic(T _type, GameObject _obj) 
    {
        prefabDic[_type] = _obj;
    }

    public virtual T[] GetTypeEnums()
    {
        return typeEnums;
    }

    public string ConvertEnumToAddress(T _type)
    {
        string enumString = _type.ToString().ToLower();
        string format = char.ToUpper(enumString[0]) + enumString.Substring(1);
        return format;
    }
}

[InitializeOnLoad]
public static class EditorResourceCleaner
{
    static EditorResourceCleaner()
    {
        EditorApplication.playModeStateChanged += ChangePlayModeState;
    }

    private static void ChangePlayModeState(PlayModeStateChange _state)
    {
        if (_state == PlayModeStateChange.ExitingPlayMode)
        {
            UnloadSceneResource();
        }
    }

    private static void UnloadSceneResource()
    {
        //GameSceneResource.instance.UnloadSceneResources();
    }
}