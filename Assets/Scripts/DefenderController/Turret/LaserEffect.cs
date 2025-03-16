using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    LineRenderer lineRenderer;
    Transform fireTrs;
    float duration = 0.1f;
    Color color;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void Init(Transform _fireTrs, Color _color)
    {
        fireTrs = _fireTrs;
        color = _color;
    }

    public void Fire(Vector3 _targetPos)
    {
        StartCoroutine(StartFire(_targetPos));
    }

    IEnumerator StartFire(Vector3 _targetPos)
    {
        lineRenderer.enabled = true;

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        lineRenderer.SetPosition(0, fireTrs.position);
        lineRenderer.SetPosition(1, _targetPos);

        yield return new WaitForSeconds(duration);

        Shared.objectPoolManager.ReturnObject(this.gameObject.name, this.gameObject);
        lineRenderer.enabled = false;
    }
}
