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
        nameText.text = DataManager.instance.playerdata.name.ToString();

        levelText.text = DataManager.instance.playerdata.level.ToString();

        expSlider.value = DataManager.instance.playerdata.curExp / DataManager.instance.playerdata.maxExp;

        goldText.text = DataManager.instance.playerdata.gold.ToString();

        gemText.text = DataManager.instance.playerdata.gem.ToString();
    }
}
