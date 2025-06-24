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
        GameManager.instance.PlayerSpawner.onSpawnPlayer += InitPlayer;
    }

    private void InitPlayer()
    {
        gunManager = GameManager.instance.PlayerSpawner.player.GetComponent<PlayerManager>().GunManager;

        gunManager.onUpdateBullet += UpdateBullet;
        GameManager.instance.PlayerSpawner.player.GetComponent<PlayerCombat>().onUseBullet += UpdateBullet;

        UpdateBullet();
    }


    private void UpdateBullet()
    {
        curBulletText.text = $"{gunManager.curBulletCount}";
        haveBulletText.text = $"{gunManager.haveBulletCount}";
        curGrenadeText.text = $"{gunManager.curGrenadeCount}";
    }
}
