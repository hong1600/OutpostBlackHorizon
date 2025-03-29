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
        gunManager = Shared.gunManager;

        gunManager.onReloading += UpdateBullet;
        Shared.playerManager.playerCombat.onUseBullet += UpdateBullet;

        UpdateBullet();
    }

    private void UpdateBullet()
    {
        curBulletText.text = $"{gunManager.curBulletCount}";
        haveBulletText.text = $"{gunManager.haveBulletCount}";
        curGrenadeText.text = $"{gunManager.curGrenadeCount}";
    }
}
