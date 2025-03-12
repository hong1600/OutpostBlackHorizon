using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public Camera screenshotCamera;
    public GameObject targetObj;

    public SpriteRenderer spriteRenderer;

    string filePath;

    public int width = 512;
    public int height = 512;

    private void Start()
    {
        filePath = Application.dataPath + "/Sprites/Obj/screenshot.png";
        Capture();
    }

    public void Capture()
    {
        RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        screenshotCamera.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);
        screenshotCamera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Ω∫≈©∏∞º¶ ¿˙¿Âµ : " + filePath);

        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
    }
}
