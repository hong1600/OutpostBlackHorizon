using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldBuilder
{
    List<Transform> GetunitSpawnPointList();
}

public class FieldBuilderDestroy : MonoBehaviour, IFieldBuilder
{
    Rigidbody rigid;

    [SerializeField] Transform parent;
    [SerializeField] GameObject fieldObj;
    GameObject unitFieldParent;
    List<GameObject> unitFieldList = new List<GameObject>();
    List<Transform> unitSpawnPointList = new List<Transform>();
    public List<Transform> GetunitSpawnPointList() { return unitSpawnPointList; }

    private void Start()
    {
        unitFieldParent = new GameObject("UnitField");
        unitFieldParent.transform.position = new Vector3(0, 0, 0);
        unitFieldParent.transform.parent = parent;

        BuildeField();
        BuildSpawnPoint();
    }

    private void BuildeField()
    {
        float x = 0;
        float z = 0;

        EGround[] groundType = (EGround[])Enum.GetValues(typeof(EGround));
        int groundIndex = 0;

        for (int i = 0; i < 6; i++) 
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject unitField = Instantiate(fieldObj, new Vector3(x, 0, z), 
                    Quaternion.identity, unitFieldParent.transform);
                unitField.layer = LayerMask.NameToLayer("Ground");
                Ground ground = unitField.AddComponent<Ground>();
                ground.eGround = groundType[groundIndex];
                unitFieldList.Add(unitField);
                rigid = unitField.GetComponent<Rigidbody>();
                rigid.isKinematic = true;

                groundIndex++;
                z -= 3;
            }

            z = 0;
            x += 3;
        }
    }

    private void BuildSpawnPoint()
    {
        for (int i = 0; i < unitFieldList.Count; i++)
        {
            GameObject unitSpawnPoint = new GameObject("unitSpawnPoint" + 1);
            unitSpawnPoint.transform.SetParent(unitFieldList[i].transform);
            unitSpawnPoint.transform.position = unitFieldList[i].transform.position + new Vector3(0, 2.5f, 0);

            unitSpawnPointList.Add(unitSpawnPoint.transform);
        }
    }
}
