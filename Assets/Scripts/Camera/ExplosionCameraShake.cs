using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCameraShake : MonoBehaviour
{
    CameraFpsShake shake;

    [SerializeField] SphereCollider sphere;
    [SerializeField] float maxShakePower = 1.5f;

    private void Start()
    {
        shake = CameraManager.instance.CameraFpsShake;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Cam"))
        {
            Shake(coll.gameObject);
        }
    }

    private void Shake(GameObject _cam)
    {
        float distance = Vector3.Distance(transform.position, _cam.transform.position);

        if (distance <= sphere.radius) 
        {
            float power = 1 - (distance / sphere.radius);
            power *= maxShakePower;

            if(shake != null) 
            {
                shake.Shake(power);
            }
        }
    }

}
