using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    [SerializeField] int bulletAmount;

    [SerializeField] MeshRenderer rend;
    Material mat;

    Color baseColor = Color.black;

    private void Awake()
    {
        mat = rend.sharedMaterial;
    }

    public int BulletAmount { get { return bulletAmount; } }
}
