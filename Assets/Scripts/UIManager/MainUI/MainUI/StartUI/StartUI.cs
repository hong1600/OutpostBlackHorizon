using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI nameText;
    public Slider expSlider;

    private void main()
    {
        nameText.text = DataMng.instance.playerData.name.ToString();

        levelText.text = DataMng.instance.playerData.level.ToString();

        expSlider.value = DataMng.instance.playerData.curExp / DataMng.instance.playerData.maxExp;

        goldText.text = DataMng.instance.playerData.gold.ToString();

        gemText.text = DataMng.instance.playerData.gem.ToString();
    }
}
