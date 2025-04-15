using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIBuild : MonoBehaviour
{
    TableTurret tableTurret;
    TableField tableField;

    Dictionary<int, UIFieldBtn> btnDic = new Dictionary<int, UIFieldBtn>();

    [SerializeField] GameObject fieldPanel;
    [SerializeField] GameObject turretPanel;

    [SerializeField] GameObject fieldBtn;
    [SerializeField] GameObject turretBtn;

    [SerializeField] Transform fieldParent;
    [SerializeField] Transform turretParent;

    private void Start()
    {
        InputManager.instance.onInputB += OpenPanel;
        tableTurret = DataManager.instance.TableTurret;
        tableField = DataManager.instance.TableField;
        CreateBtn();
    }

    public void OpenPanel()
    {
        fieldPanel.SetActive(!fieldPanel.activeSelf);
        turretPanel.SetActive(!turretPanel.activeSelf);
    }

    private void CreateBtn()
    {
        List<int> fieldKeyList = tableField.Dictionary.Keys.ToList();
        List<int> turretKeyList = tableTurret.Dictionary.Keys.ToList();

        for (int i = 0; i < fieldKeyList.Count; i++)
        {
            TableField.Info info = tableField.Dictionary[fieldKeyList[i]];
            GameObject slot = Instantiate(fieldBtn, fieldParent);
            UIFieldBtn btn = slot.GetComponent<UIFieldBtn>();
            btnDic.Add(info.ID, btn);
            btn.InitFieldBtn(info);
        }

        for (int i = 0; i < turretKeyList.Count; i++)
        {
            TableTurret.Info info = tableTurret.Dictionary[turretKeyList[i]];
            GameObject slot = Instantiate(turretBtn, turretParent);
            UITurretBtn btn = slot.GetComponent<UITurretBtn>();
            btn.InitTurretBtn(info);
        }
    }

    public void DecreaseFieldAmount(int _id, int _amount)
    {
        if (btnDic.ContainsKey(_id))
        {
            btnDic[_id].DecreaseAmount(_amount);
        }
    }
}
