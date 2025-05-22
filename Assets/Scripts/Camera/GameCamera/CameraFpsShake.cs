using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFpsShake : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] float shakeDuration = 0.2f;

    Vector3 originPos;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void Init()
    {
        originPos = mainCam.transform.localPosition;
    }

    private void OnEnable()
    {
        originPos = mainCam.transform.localPosition;
    }

    public void Shake(float _power = 0.4f)
    {
        StopAllCoroutines();
        StartCoroutine(StartShake(_power));
    }

    IEnumerator StartShake(float _power)
    {
        float time = 0f;

        while (time < shakeDuration) 
        {
            float x = (Mathf.PerlinNoise(Time.time * 5f, 0f) - 0.5f) * _power * 2;
            float y = (Mathf.PerlinNoise(0f, Time.time * 5f) - 0.5f) * _power;

            mainCam.transform.localPosition = originPos + new Vector3(x, y, 0f);

            time += Time.fixedDeltaTime;
            yield return null;
        }

        mainCam.transform.localPosition = originPos;
    }
}
