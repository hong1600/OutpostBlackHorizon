using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICloseBtn : MonoBehaviour
{
    [SerializeField] GameObject functionPanel;

    public void OnClickCloseBtn()
    {
        for (int i = 0; functionPanel.transform.childCount > 0; i++) 
        {
            Transform child = functionPanel.transform.GetChild(i);

            if (child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
