using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class MissingScriptFinder : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow<MissingScriptFinder>("Missing Script Finder");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in Scenes"))
        {
            FindMissingScriptsInScenes();
        }
        if (GUILayout.Button("Find Missing Scripts in Prefabs"))
        {
            FindMissingScriptsInPrefabs();
        }
    }

    static void FindMissingScriptsInScenes()
    {
        int goCount = 0, missingCount = 0;
        string result = "";

        for (int i = 0; i < EditorSceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            var rootObjects = scene.GetRootGameObjects();

            foreach (var go in rootObjects)
            {
                var allTransforms = go.GetComponentsInChildren<Transform>(true);
                foreach (var t in allTransforms)
                {
                    goCount++;
                    var components = t.gameObject.GetComponents<Component>();
                    foreach (var comp in components)
                    {
                        if (comp == null)
                        {
                            missingCount++;
                            result += $"Missing script found in scene '{scene.name}', GameObject '{t.gameObject.name}'\n";
                        }
                    }
                }
            }
        }

        Debug.Log($"Searched {goCount} GameObjects, found {missingCount} missing scripts.\n{result}");
    }

    static void FindMissingScriptsInPrefabs()
    {
        int goCount = 0, missingCount = 0;
        string result = "";

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (var guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            var allTransforms = prefab.GetComponentsInChildren<Transform>(true);
            foreach (var t in allTransforms)
            {
                goCount++;
                var components = t.gameObject.GetComponents<Component>();
                foreach (var comp in components)
                {
                    if (comp == null)
                    {
                        missingCount++;
                        result += $"Missing script found in prefab '{path}', GameObject '{t.gameObject.name}'\n";
                    }
                }
            }
        }

        Debug.Log($"Searched {goCount} GameObjects, found {missingCount} missing scripts.\n{result}");
    }
}
