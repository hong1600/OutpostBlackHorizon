using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoom : MonoBehaviour
{
    private void Update()
    {
        CursorLock();
    }

    private void CursorLock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
