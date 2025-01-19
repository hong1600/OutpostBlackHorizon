using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundPanel : MonoBehaviour
{
    [SerializeField] Image bgmImg;
    [SerializeField] Image sfxImg;
    [SerializeField] bool bgm;
    [SerializeField] bool sfx;

    private void Awake()
    {
        bgm = false;
        sfx = false;
    }

    public void ClickBgmBtn()
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

    public void ClickSfxBtn()
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
