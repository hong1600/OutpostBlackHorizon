using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public Image bgmImg;
    public Image sfxImg;
    public bool bgm;
    public bool sfx;

    private void Start()
    {
        bgm = false;
        sfx = false;
    }

    public void showPanelOpen(GameObject targetPanel)
    {
        if (targetPanel.activeSelf == false)
        {
            targetPanel.SetActive(true);
            targetPanel.transform.localScale = Vector3.zero;
            targetPanel.transform.DOScale(Vector3.one, 0.2f);
        }
    }

    public void closeBtn(GameObject button)
    {
        Transform buttonParent = button.transform.parent;

        if (buttonParent != null)
        {
            buttonParent.gameObject.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                buttonParent.gameObject.SetActive(false);
            });
        }
    }
    public void bgmBtn()
    {
        if (bgm == false)
        {
            bgmImg.color = new Color(1, 0, 0, 1);
            bgm = true;
        }
        else
        {
            bgmImg.color = new Color(1, 0, 0, 0);
            bgm = false;
        }
    }

    public void sfxBtn()
    {
        if (sfx == false)
        {
            sfxImg.color = new Color(1, 0, 0, 1);
            sfx = true;
        }
        else
        {
            sfxImg.color = new Color(1, 0, 0, 0);
            sfx = false;
        }
    }
}
