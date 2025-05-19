using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropshipCut : MonoBehaviour, ICutScene
{
    [Header("Camera")]
    [SerializeField] GameObject camObj;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] AnimationClip clip;

    [Header("Movement Setting")] 
    [SerializeField] List<Transform> dropshipPos = new List<Transform>();
    Terrain terrain;

    [Header("Dropship Setting")]
    [SerializeField] List<GameObject> dropshipList = new List<GameObject>();
    [SerializeField] GameObject dropship;
    Dictionary<EEnemyDropship, Transform> dropshipDic;

    [Header("Camera Pos")]
    [SerializeField] Vector3 startPos;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;

        dropshipDic = new Dictionary<EEnemyDropship, Transform>
        {
            { EEnemyDropship.DROPSHIP1, dropshipPos[0]},
            { EEnemyDropship.DROPSHIP2, dropshipPos[1]},
            { EEnemyDropship.DROPSHIP3, dropshipPos[2]},
            { EEnemyDropship.DROPSHIP4, dropshipPos[3]},
        };
    }

    public void Play()
    {
        for (int i = 0; i < dropshipList.Count; i++)
        {
            if (dropshipDic.TryGetValue
                (dropshipList[i].GetComponent<EnemyDropShip>().EEnemyDropship, out Transform selectedPos))
            {
                MoveDropShip(dropshipList[i], selectedPos);
            }
        }

        StartCoroutine(StartCutScene());
    }

    private void MoveDropShip(GameObject _dropship, Transform _pos)
    {
        _dropship.SetActive(true);

        AudioManager.instance.PlaySfx
                    (ESfx.UFOLAND, _dropship.transform.position, _dropship.transform);

        Vector3 dropPos = new Vector3(_pos.position.x, terrain.SampleHeight(_pos.position) + 3, _pos.position.z);

        _dropship.transform.DOMove(dropPos, 7).SetEase(Ease.Linear);
    }

    IEnumerator StartCutScene()
    {
        camObj.SetActive(true);

        cam.m_Follow = dropshipList[1].transform;
        cam.m_LookAt = dropshipList[1].transform;

        GameManager.instance.Timer.SetTimer(false);
        GameManager.instance.ViewState.SwitchNone();

        StartCoroutine(GameUI.instance.StartBlackout(1));

        yield return new WaitForSeconds(1);

        CameraManager.instance.CutScene.PlayClip(clip);

        yield return new WaitForSeconds(8);

        camObj.SetActive(false);
        EnemyManager.instance.EnemySpawner.IsSpawn = true;

        yield return new WaitForSeconds(1);

        GameManager.instance.Timer.SetTimer(true);
        GameManager.instance.ViewState.SetViewState(EViewState.FPS);
    }
}
