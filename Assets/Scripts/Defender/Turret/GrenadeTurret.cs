using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTurret : MonoBehaviour
{
    PlayerManager playerManager;
    CameraFpsShake cameraFpsShake;
    BulletPool bulletPool;

    [SerializeField] Transform eyeTrs;
    [SerializeField] Transform fireTrs;
    [SerializeField] GameObject shootObj;

    bool isAttack;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        cameraFpsShake = CameraManager.instance.CameraFpsShake;
        bulletPool = ObjectPoolManager.instance.BulletPool;
    }

    public void ChangeTurretView()
    {
        CameraManager.instance.CameraTurretMove.turretObj = this.gameObject;
        CameraManager.instance.CameraTurretMove.shootObj = shootObj;
        StartCoroutine(GameManager.instance.ViewState.SwitchInTurret(eyeTrs.position, eyeTrs.rotation));
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && !isAttack ) 
        {
            StartCoroutine(StartAttackGrenade());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameManager.instance.ViewState.SetViewState(EViewState.FPS);
        }
    }

    IEnumerator StartAttackGrenade()
    {
        isAttack = true;
        AudioManager.instance.PlaySfx(ESfx.GRENADETURRET, transform.position, null);
        GameObject obj = bulletPool.FindBullet(EBullet.TURRETGRENADE, fireTrs.position, fireTrs.rotation);
        TurretGrenade bullet = obj.GetComponent<TurretGrenade>();
        bullet.Init(null, 300, 100);

        cameraFpsShake.Init();
        cameraFpsShake.Shake();

        yield return new WaitForSeconds(0.25f);

        isAttack = false;
    }

}
