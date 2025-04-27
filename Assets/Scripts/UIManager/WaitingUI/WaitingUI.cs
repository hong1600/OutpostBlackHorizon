using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUI : MonoBehaviour
{
    [SerializeField] Image mapImg;
    [SerializeField] TextMeshProUGUI mapNameText;
    [SerializeField] TextMeshProUGUI mapLevel;
    [SerializeField] TextMeshProUGUI mapDescText;
    [SerializeField] TextMeshProUGUI fieldAmountText;
    [SerializeField] TextMeshProUGUI outpostText;

    [SerializeField] string[] mapNames;
    [SerializeField] Sprite[] mapSprites;
    [SerializeField] string[] mapDescs;

    public int curMapIndex = 1;

    private void Start()
    {
        mapNameText.text = mapNames[curMapIndex - 1];
        mapLevel.text = $"난이도 : {curMapIndex}";
        fieldAmountText.text = DataManager.instance.TableMap.GetMapData(700 + curMapIndex).fieldAmount.ToString();
        mapDescText.text = mapDescs[curMapIndex - 1];
        mapImg.sprite = mapSprites[curMapIndex - 1];
        outpostText.text = $"전초 기지 No.{curMapIndex}";
    }

    public void UpdateMap(int _index)
    {
        curMapIndex += _index;

        if (curMapIndex > 2 || curMapIndex < 1) return;

        switch (curMapIndex)
        {
            case 1:
                ChangeMap();
                break;
            case 2:
                ChangeMap();
                break;
        }
    }

    private void ChangeMap()
    {
        mapNameText.text = mapNames[curMapIndex - 1];
        mapLevel.text = $"난이도 : {curMapIndex}";
        mapDescText.text = mapDescs[curMapIndex - 1];
        mapImg.sprite = mapSprites[curMapIndex - 1];
        fieldAmountText.text = DataManager.instance.TableMap.GetMapData(700 + curMapIndex).fieldAmount.ToString();
        outpostText.text = $"전초 기지 No.{curMapIndex}";
    }
}
