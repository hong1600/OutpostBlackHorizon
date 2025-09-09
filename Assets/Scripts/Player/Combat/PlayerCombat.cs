using DG.Tweening;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] protected GunManager gunManager;

    protected IBulletPool bulletPool;
    protected CameraFpsShake cameraFpsShake;
    CameraFpsZoom cameraFpsZoom;
    protected GunMovement gunMovement;

    public event Action onUseBullet;

    [SerializeField] internal bool isAttack = false;
    [SerializeField] protected GameObject muzzleFlash;
    [SerializeField] protected Transform fireTrs;

    [SerializeField] protected float bulletDmg;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float grenadeSpd;

    public GameObject curWeapon;

    [SerializeField] Vector3 showPos;
    [SerializeField] Vector3 hideOffset;
    float swapDuration = 0.15f;
    public bool isSwapping { get; private set; } = false;

    public List<GameObject> weaponList = new List<GameObject>();

    private void Awake()
    {
        curWeapon = weaponList[0];
    }

    private void Start()
    {
        bulletPool = Shared.Instance.poolManager.BulletPool;
        cameraFpsShake = CameraManager.instance.CameraFpsShake;
        cameraFpsZoom = CameraManager.instance.CameraFpsZoom;
        gunMovement = gunManager.GunMovement;

        InputManager.instance.onInput1 += SwapWeapon;
        InputManager.instance.onInput2 += SwapWeapon;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButton(0) && cameraFpsShake.enabled == true && !isAttack && !gunManager.isReloading) 
        {
            StartCoroutine(StartFireRifle());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !gunManager.isReloading && gunManager.curGrenadeCount > 0) 
        {
            StartCoroutine(StartFireGrenade());
        }
    }

    protected virtual IEnumerator StartFireRifle()
    {
        isAttack = true;

        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        gunManager.UseBullet();

        if (!cameraFpsZoom.isZoom)
        {
            gunMovement.RecoilGun();
        }

        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERBULLET, fireTrs.position, fireTrs.rotation);
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.Init(null, bulletDmg, bulletSpd, EBulletType.PLAYER);

        cameraFpsShake.Shake();

        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    protected virtual IEnumerator StartFireGrenade()
    {
        isAttack = true;

        AudioManager.instance.PlaySfx(ESfx.GRENADESHOT, transform.position, null);

        muzzleFlash.SetActive(true);

        gunManager.UseGrenade();
        gunMovement.RecoilGun();

        GameObject obj = bulletPool.FindBullet(EBullet.PLAYERGRENADE, fireTrs.position, fireTrs.rotation);
        PlayerGrenade bullet = obj.GetComponent<PlayerGrenade>();
        bullet.Init(null, 100, grenadeSpd);

        cameraFpsShake.Shake();

        onUseBullet?.Invoke();

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.SetActive(false);

        isAttack = false;
    }

    protected void InvokeUseBullet()
    {
        onUseBullet?.Invoke();
    }

    private void SwapWeapon(int _weaponIndex)
    {
        if (isSwapping || gunManager.isReloading) return;

        if (cameraFpsZoom.isZoom)
        {
            cameraFpsZoom.ZoomCamera();
        }

        EPlayerWeapon weapon = (EPlayerWeapon)_weaponIndex;

        switch (weapon) 
        {
            case EPlayerWeapon.RIFLE:
                StartCoroutine(StartSwapWeapon(weaponList[0], weaponList[1]));
                break;
            case EPlayerWeapon.PISTOL:
                StartCoroutine(StartSwapWeapon(weaponList[1], weaponList[0]));
                break;
        }
    }

    IEnumerator StartSwapWeapon(GameObject _newWeapon, GameObject _oldWeapon)
    {
        if (_newWeapon == null || _oldWeapon == null) yield break;

        isSwapping = true;

        _oldWeapon.transform.DOKill(true);
        _newWeapon.transform.DOKill(true);

        Vector3 hidePos = _oldWeapon.transform.position + hideOffset;

        Tween t = _oldWeapon.transform.DOLocalMove(hidePos, swapDuration).SetEase(Ease.InSine);

        yield return t.WaitForCompletion();

        _oldWeapon.SetActive(false);

        _newWeapon.SetActive(true);

        t = _newWeapon.transform.DOLocalMove(showPos, swapDuration).SetEase(Ease.OutSine);

        yield return t.WaitForCompletion();

        curWeapon = _newWeapon;

        isSwapping = false;
    }
}
