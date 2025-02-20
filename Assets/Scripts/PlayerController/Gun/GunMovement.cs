using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    Vector3 originPos;
    Quaternion originRot;

    //[SerializeField] float swayAmount = 0.02f;
    //[SerializeField] float smoothFactor = 2f;

    [SerializeField] float walkSwaySpeed = 10f;
    [SerializeField] float walkSwayAmount = 0.01f;
    float timer = 0f;

    [SerializeField] float recoilAmount = 2f;
    [SerializeField] float recoilDuration = 0.1f;
    [SerializeField] float recoilRecovery = 5f;
    bool isRecoil;

    private void OnEnable()
    {
        //InputMng.onInputMouse += SwayGun;
        InputMng.onInputKey += WalkSwayGun;
    }

    private void OnDisable()
    {
        //InputMng.onInputMouse -= SwayGun;
        InputMng.onInputKey -= WalkSwayGun;
    }

    public void InitPos()
    {
        originPos = transform.localPosition;
        originRot = transform.localRotation;
    }

    //private void SwayGun(Vector2 _inputMouse)
    //{
    //    if (Shared.gameMng.iViewState.GetViewState() == EViewState.TOP || !Shared.cameraMng.isArrive) return;

    //    float moveX = _inputMouse.x * swayAmount;
    //    float moveY = _inputMouse.y * swayAmount;

    //    Vector3 swayPos = new Vector3(-moveX, -moveY, 0);
    //    transform.localPosition = Vector3.Lerp(transform.localPosition,
    //        originPos + swayPos, Time.deltaTime * smoothFactor);
    //}

    private void WalkSwayGun(Vector2 _inputKey)
    {
        if (Shared.gameMng.iViewState.GetViewState() == EViewState.TOP || !Shared.cameraMng.isArrive) return;

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
