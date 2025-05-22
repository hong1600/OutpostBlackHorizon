using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMissile : MonoBehaviour
{
    Camera mainCam;
    SphereCollider sphere;

    BulletPool bulletPool;
    EffectPool effectPool;
    ViewState viewState;

    [SerializeField] Transform spawnPos;

    GameObject missile;
    Transform missileEye;

    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    public float coolTime { get; private set; } = 0f;
    public float maxCoolTime { get; private set; } = 60f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        bulletPool = ObjectPoolManager.instance.BulletPool;
        effectPool = ObjectPoolManager.instance.EffectPool;
        viewState = GameManager.instance.ViewState;
    }

    private void Update()
    {
        if (coolTime >= 0)
        {
            coolTime -= Time.deltaTime;
        }

        if(missile != null) 
        {
            MoveMissile();
            TurnMissile();
            CheckObj();
        }
    }

    public void FireMissile()
    {
        if (coolTime <= 0)
        {
            coolTime = 60f;

            viewState.SwitchNone();

            missile = bulletPool.FindBullet(EBullet.GUIDEMISSILE, spawnPos.position, Quaternion.identity);
            missileEye = missile.transform.GetChild(0);
            sphere = missile.GetComponent<SphereCollider>();

            mainCam.transform.parent = missile.transform;
            mainCam.transform.position = missileEye.position;
            mainCam.transform.rotation = Quaternion.identity;
        }
    }

    private void MoveMissile()
    {
        missile.transform.position += missile.transform.forward * speed * Time.deltaTime;
    }

    private void TurnMissile()
    {
        Vector2 mousePos = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        missile.transform.Rotate(Vector3.up * mousePos.x * turnSpeed, Space.World);

        missile.transform.Rotate(Vector3.right * -mousePos.y * turnSpeed, Space.Self);
    }

    private void CheckObj()
    {
        if (Physics.SphereCast(missile.transform.position, sphere.radius * missile.transform.lossyScale.x,
            missile.transform.forward, out RaycastHit hit, speed * Time.deltaTime,
            ~LayerMask.GetMask("EnemySensor", "Bullet", "Effect")))
        {
            StartCoroutine(StartExplosion());
            viewState.SetViewState(EViewState.FPS);
        }
    }

    IEnumerator StartExplosion()
    {
        Vector3 explosionPos = missile.transform.position;

        StartCoroutine(GameUI.instance.StartBlackout(1.52f));

        yield return null;

        bulletPool.ReturnBullet(EBullet.GUIDEMISSILE, missile);

        yield return new WaitForSeconds(1.52f);

        effectPool.FindEffect(EEffect.GUIDEMISSILEEXPLOSION, explosionPos, Quaternion.identity);
    }
}
