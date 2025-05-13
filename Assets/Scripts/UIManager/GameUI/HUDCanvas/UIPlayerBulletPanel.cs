using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerBulletPanel : MonoBehaviour
{
    GunManager gunManager;

    [SerializeField] TextMeshProUGUI curBulletText;
    [SerializeField] TextMeshProUGUI haveBulletText;
    [SerializeField] TextMeshProUGUI curGrenadeText;

    private void Start()
    {
        gunManager = GunManager.instance;

        gunManager.onUpdateBullet += UpdateBullet;
        PlayerManager.instance.playerCombat.onUseBullet += UpdateBullet;

        UpdateBullet();
    }

    private void UpdateBullet()
    {
        curBulletText.text = $"{gunManager.curBulletCount}";
        haveBulletText.text = $"{gunManager.haveBulletCount}";
        curGrenadeText.text = $"{gunManager.curGrenadeCount}";
    }
}
