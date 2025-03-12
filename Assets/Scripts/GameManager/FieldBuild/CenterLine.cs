using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    Terrain terrain;

    public int point { get; private set; } = 50;
    public float radius { get; private set; } = 25f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        terrain = Terrain.activeTerrain;
    }

    void Start()
    {
        DrawCircle();
    }

    private void DrawCircle()
    {
        lineRenderer.positionCount = point + 1;
        float angle = 0f;

        for (int i = 0; i < point + 1; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += (2 * Mathf.PI) / point;
        }
    }
}
