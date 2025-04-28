using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Center : MonoBehaviour, ITakeDmg
{
    Terrain terrain;
    GameState gameState;
    EffectPool effectPool;

    [SerializeField] Renderer rend;
    [SerializeField] Material hitMat;
    Material originMat;

    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    GameObject explosionEffect;

    bool isDestroy = false;

    [Header("CenterLine")]
    [SerializeField] LineRenderer lineRenderer;
    public int point { get; private set; } = 50;
    public float radius { get; private set; } = 25f;

    private void Awake()
    {
        originMat = rend.material;
        terrain = Terrain.activeTerrain;
    }

    void Start()
    {
        gameState = GameManager.instance.GameState;
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

    public void TakeDmg(float _dmg, bool _isHead)
    {
        curHp -= _dmg;

        rend.material = hitMat;

        Invoke(nameof(UpdateMat), 0.2f);

        if (curHp <= 0 && !isDestroy)
        {
            isDestroy = true;

            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

            explosionEffect = effectPool.FindEffect(EEffect.ENEMYEXPLOSION, transform.position, Quaternion.identity);

            Invoke(nameof(ReturnEffect), 1);

            gameState.SetGameState(EGameState.GAMEOVER);
        }
    }

    private void UpdateMat()
    {
        rend.material = originMat;
    }

    private void ReturnEffect()
    {
        effectPool.ReturnEffect(EEffect.ENEMYEXPLOSION, explosionEffect);

        Destroy(this.gameObject);
    }
}
