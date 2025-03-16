using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveField : MonoBehaviour
{
    [SerializeField] List<GameObject> startSelectUnitList = new List<GameObject>();
    [SerializeField] List<GameObject> curSelectUnitList = new List<GameObject>();

    public void CheckUnitField(GameObject _startSelectField, GameObject _curSelectField)
    {
        if (_startSelectField.transform.childCount <= 0) return;

        startSelectUnitList.Clear();
        curSelectUnitList.Clear();

        for (int i = 0; i < _startSelectField.transform.childCount; i++)
        {
            startSelectUnitList.Add(_startSelectField.transform.GetChild(i).gameObject);
        }

        if (_curSelectField.transform.childCount <= 0) return;

        for (int i = 0;i < _curSelectField.transform.childCount; i++) 
        {
            curSelectUnitList.Add(_curSelectField.transform.GetChild(i).gameObject);
        }

        MoveUnitField(_startSelectField.transform, _curSelectField.transform);
    }

    public void MoveUnitField(Transform _startField, Transform _curField)
    {
        Transform startParent = _startField;
        Transform curParent = _curField;

        Vector3 twoUnit1Pos = new Vector3(-0.5f, 0, 0);
        Vector3 twoUnit2Pos = new Vector3(0.5f, 0, 0);

        Vector3 threeUnit1Pos = new Vector3(0, 0, 0.4f);
        Vector3 threeUnit2Pos = new Vector3(-0.6f, 0, -0.4f);
        Vector3 threeUnit3Pos = new Vector3(0.6f, 0, -0.4f);

        SetUnitPos(startSelectUnitList, curParent);
        SetUnitPos(curSelectUnitList, startParent);

        void SetUnitPos(List<GameObject> _unitList, Transform _parent)
        {
            for(int i = 0; i < _unitList.Count; i++)
            {
                GameObject unit = _unitList[i];
                unit.transform.SetParent(_parent);

                Vector3 pos = _parent.position;

                switch (_unitList.Count)
                {
                    case 1:
                        pos = _parent.position;
                        break;
                    case 2:
                        if (i == 0) pos += twoUnit1Pos;
                        else if (i == 1) pos += twoUnit2Pos;
                        break;
                    case 3:
                        if (i == 0) pos += threeUnit1Pos;
                        else if(i == 1) pos += threeUnit2Pos;
                        else if (i == 2) pos += threeUnit3Pos;
                        break;
                    default:
                        return;
                }

                unit.transform.position = pos;
            }
        }
    }
}
