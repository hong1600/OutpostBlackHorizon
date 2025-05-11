using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECutScene
{
    ENEMYDROPSHIP,
    FINISH,
}

public class CutScene : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] List<MonoBehaviour> cutSceneList = new List<MonoBehaviour>();
    Dictionary<ECutScene, ICutScene> cutSceneDic = new Dictionary<ECutScene, ICutScene>();

    private void Start()
    {
        AddCutScene();
    }

    private void AddCutScene()
    {
        for (int i = 0; i < cutSceneList.Count; i++)
        {
            ICutScene iCutScene = cutSceneList[i].GetComponent<ICutScene>();

            ECutScene cutSceneKey = (ECutScene)i;

            if (!cutSceneDic.ContainsKey(cutSceneKey))
            {
                cutSceneDic.Add(cutSceneKey, iCutScene);
            }
        }
    }

    public void PlayCutScene(ECutScene _eCutScene)
    {
        if (cutSceneDic.ContainsKey(_eCutScene))
        {
            ICutScene iCutScene = cutSceneDic[_eCutScene];
            iCutScene.Play();
        }
    }

    public void PlayClip(AnimationClip _clip)
    {
        anim.Play(_clip.name);
    }
}
