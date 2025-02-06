using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] float swayAmount = 0.02f;
    [SerializeField] float smoothFactor = 2f;

    [SerializeField] float walkSwaySpeed = 10f;
    [SerializeField] float walkSwayAmount = 0.01f;
    float timer = 0;

    Vector3 initPos;

    private void OnEnable()
    {
        InputMng.onInputMouse += SwayGun;
        InputMng.onInputKey += WalkSwayGun;
    }

    private void OnDisable()
    {
        InputMng.onInputMouse -= SwayGun;
        InputMng.onInputKey -= WalkSwayGun;
    }

    public void InitPos()
    {
        initPos = transform.localPosition;
    }

    private void SwayGun(Vector2 _inputMouse)
    {
        if (Shared.gameMng.iViewState.GetViewState() == EViewState.TOP || !Shared.cameraMng.isArrive) return;

        float moveX = _inputMouse.x * swayAmount;
        float moveY = _inputMouse.y * swayAmount;

        Vector3 swayPos = new Vector3(-moveX, -moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition,
            initPos + swayPos, Time.deltaTime * smoothFactor);
    }

    private void WalkSwayGun(Vector3 _inputKey)
    {
        if (Shared.gameMng.iViewState.GetViewState() == EViewState.TOP || !Shared.cameraMng.isArrive) return;

        if (_inputKey.x != 0 || _inputKey.z != 0)
        {
            timer += Time.deltaTime * walkSwaySpeed;
            float walkSwayOffset = Mathf.Sin(timer) * walkSwayAmount;
            transform.localPosition = initPos + new Vector3(0, walkSwayOffset, 0);
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initPos, Time.deltaTime * walkSwaySpeed);
        }
    }
}
