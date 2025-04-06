using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIBuild : MonoBehaviour
{
    TableTurret tableTurret;

    [SerializeField] GameObject fieldPanel;
    [SerializeField] GameObject turretPanel;

    [SerializeField] GameObject fieldBtn;
    [SerializeField] GameObject turretBtn;

    [SerializeField] List<FieldData> fieldData;

    [SerializeField] Transform fieldParent;
    [SerializeField] Transform turretParent;


    private void Start()
    {
        InputManager.instance.onInputB += OpenPanel;
        tableTurret = DataManager.instance.TableTurret;
        CreateFieldBtn();
        CreateTurretBtn();
    }

    private void OpenPanel()
    {
        fieldPanel.SetActive(!fieldPanel.activeSelf);
    }

    private void CreateFieldBtn()
    {
        for (int i = 0; i < fieldData.Count; i++) 
        {
            GameObject slot = Instantiate(fieldBtn, fieldParent);
            UIFieldBtn btn = slot.GetComponent<UIFieldBtn>();
            btn.InitFieldBtn(fieldData[i]);
        }
    }

    private void CreateTurretBtn()
    {
        List<int> keys = tableTurret.Dictionary.Keys.ToList();

        for (int i = 0; i < keys.Count; i++)
        {
            TableTurret.Info info = tableTurret.Dictionary[keys[i]];
            GameObject slot = Instantiate(turretBtn, turretParent);
            UITurretBtn btn = slot.GetComponent<UITurretBtn>();
            btn.InitTurretBtn(info);
        }
    }
}
