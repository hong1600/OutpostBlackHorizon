using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerBulletPanel : MonoBehaviour
{
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] GunMng gunMng;

    [SerializeField] TextMeshProUGUI curBulletText;
    [SerializeField] TextMeshProUGUI haveBulletText;
    [SerializeField] TextMeshProUGUI curGrenadeText;

    private void Start()
    {
        playerCombat.onUseBullet += UpdateBullet;

        UpdateBullet();
    }

    private void UpdateBullet()
    {
        curBulletText.text = $"{gunMng.curBulletCount}";
        haveBulletText.text = $"{gunMng.haveBulletCount}";
    }

    private void UpdateGrenade()
    {

    }
}
