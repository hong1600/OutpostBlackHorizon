using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    ViewState viewState;
    CameraTopToFps cameraTopToFps;

    Vector3 originPos;
    Quaternion originRot;

    [SerializeField] float walkSwaySpeed = 10f;
    [SerializeField] float walkSwayAmount = 0.01f;
    float timer = 0f;

    [Header("RifleSetting")]
    [SerializeField] float recoilAmount = 0.015f;
    [SerializeField] float recoilDuration = 0.01f;
    [SerializeField] float recoilRecovery = 10f;
    bool isRecoil;

    private void Start()
    {
        InputManager.instance.onInputKey += WalkSwayGun;
        cameraTopToFps = Shared.cameraManager.CameraTopToFps;
        viewState = Shared.gameManager.ViewState;
    }

    private void OnDisable()
    {
        InputManager.instance.onInputKey -= WalkSwayGun;
    }

    public void InitPos()
    {
        originPos = transform.localPosition;
        originRot = transform.localRotation;
    }

    private void WalkSwayGun(Vector2 _inputKey)
    {
        if (viewState.CurViewState ==  EViewState.TOP || !cameraTopToFps.isArrive) return;

        if (_inputKey.x != 0 || _inputKey.y != 0)
        {
            timer += Time.deltaTime * walkSwaySpeed;
            float walkSwayOffset = Mathf.Sin(timer) * walkSwayAmount;
            transform.localPosition = originPos + new Vector3(0, walkSwayOffset, 0);
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp
                (transform.localPosition, originPos, Time.deltaTime * walkSwaySpeed * 2);
        }
    }

    public void RecoilGun()
    {
        if (!isRecoil)
        {
            StartCoroutine(StartRecoil());
        }
    }

    IEnumerator StartRecoil()
    {
        isRecoil = true;

        Vector3 recoilOffset = Vector3.back * recoilAmount;
        Quaternion recoilRot = Quaternion.Euler(-recoilAmount, Random.Range(-recoilAmount, recoilAmount), 0);

        transform.localPosition += recoilOffset;
        transform.localRotation *= recoilRot;

        yield return new WaitForSeconds(recoilDuration);

        while (Vector3.Distance(transform.localPosition, originPos) > 0.01f ||
            Quaternion.Angle(transform.localRotation, originRot) > 0.1f)
        {
            transform.localPosition = Vector3.Lerp
                (transform.localPosition, originPos, Time.deltaTime * recoilRecovery);

            transform.localRotation = Quaternion.Lerp
                (transform.localRotation, originRot, Time.deltaTime * recoilRecovery);

            yield return null;
        }

        transform.localPosition = originPos;
        transform.localRotation = originRot;

        isRecoil = false;
    }
}
