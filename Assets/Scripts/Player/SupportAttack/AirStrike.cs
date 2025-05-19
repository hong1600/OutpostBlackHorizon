using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    [SerializeField] GameObject jetPrefab;
    List<GameObject> jetObjList = new List<GameObject>();
    [SerializeField] Transform[] StartPos;
    [SerializeField] Transform[] EndPos;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(jetPrefab, StartPos[i].transform.position, Quaternion.Euler(0, -90f, 0));
            jetObjList.Add(go);
            jetObjList[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayAirStrike();
        }
    }

    public void PlayAirStrike()
    {
        StartCoroutine(StartAirStrike());
    }

    IEnumerator StartAirStrike()
    {
        for (int i = 0; i < jetObjList.Count; i++)
        {
            GameObject go = jetObjList[i];

            go.SetActive(true);

            go.transform.position = StartPos[i].transform.position;

            AudioManager.instance.PlaySfx(ESfx.AIRSKRIKE, go.transform.position, go.transform, 10, 600);

            go.transform.DOMove(EndPos[i].position, 6).SetEase(Ease.Linear);

            Jet jet = go.GetComponent<Jet>();
        }

        yield return new WaitForSeconds(2.5f);

        for (int i = 0; i < jetObjList.Count; i++)
        {
            Jet jet = jetObjList[i].GetComponent<Jet>();
            jet.Drop();
        }

        yield return new WaitForSeconds(3);

        ReturnObj();
    }

    private void ReturnObj()
    {
        for (int i = 0; i < jetObjList.Count; i++)
        {
            jetObjList[i].SetActive(false);
        }
    }
}
