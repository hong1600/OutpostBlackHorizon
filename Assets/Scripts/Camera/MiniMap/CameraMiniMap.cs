using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] Transform target;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 pos = new Vector3(target.position.x, transform.position.y, target.position.z + 10);
            transform.position = pos;
        }
    }
}
