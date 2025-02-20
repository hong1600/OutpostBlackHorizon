using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float turnSpeed;
    float veticalRot = 0;

    private void LateUpdate()
    {
        Turn();
    }

    private void Turn()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * turnSpeed;
        float mouseY = Input.GetAxisRaw("Mouse Y") * turnSpeed;

        veticalRot -= mouseY;
        veticalRot = Mathf.Clamp(veticalRot, -60f, 60f);

        player.transform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(veticalRot, 0f, 0f);
    }
}
