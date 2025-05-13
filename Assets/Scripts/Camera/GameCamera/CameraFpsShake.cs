using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFpsShake : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] float shakeMagnitude = 0.1f;

    Vector3 originPos;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void Init(Vector3 _pos)
    {
        originPos = _pos;
    }

    private void OnEnable()
    {
        originPos = mainCam.transform.localPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(StartShake());
    }

    IEnumerator StartShake()
    {
        float time = 0f;

        while (time < shakeDuration) 
        {
            float x = (Mathf.PerlinNoise(Time.time * 5f, 0f) - 0.5f) * shakeMagnitude * 2;
            float y = (Mathf.PerlinNoise(0f, Time.time * 5f) - 0.5f) * shakeMagnitude;

            mainCam.transform.localPosition = originPos + new Vector3(x, y, 0f);

            time += Time.fixedDeltaTime;
            yield return null;
        }

        mainCam.transform.localPosition = originPos;
    }
}
