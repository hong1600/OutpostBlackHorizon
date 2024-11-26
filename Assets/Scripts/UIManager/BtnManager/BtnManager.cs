using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public static BtnManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void openPanel(GameObject targetPanel)
    {
        if (targetPanel.activeSelf == false)
        {
            targetPanel.SetActive(true);
            targetPanel.transform.localScale = Vector3.zero;
            targetPanel.transform.DOScale(Vector3.one, 0.2f);
        }
    }

    //public void clickPerClostBtn(GameObject perPanel)
    //{
    //    if (perPanel.activeSelf)
    //    {
    //        closeBtn(perPanel);
    //    }
    //}

    //public void clickUnitDcClostBtn(GameObject unitDcPanel)
    //{
    //    if (unitDcPanel.activeSelf)
    //    {
    //        closeBtn(unitDcPanel);
    //    }
    //}

}
