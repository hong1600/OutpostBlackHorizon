using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Shared
{
    public static TableMgr TableMgr;

    public static TableMgr InitTableMgr()
    {
        if (TableMgr == null)
        {
            TableMgr = new TableMgr();
            TableMgr.Init();
        }

        return TableMgr;
    }
}

public enum ECharacter
{
    a,
    b,
}

public class Character : MonoBehaviour
{
    public Text text;
    public string a;

    private void Awake()
    {
        TableMgr mng = new TableMgr();

        mng.Init();

        Table_Character.Info info = mng.Character.Get((int)ECharacter.b);

        text.text = info.Name.ToString();
    }
}
