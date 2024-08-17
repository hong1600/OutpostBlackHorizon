using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Slider expSlider;

    private void Update()
    {
        Name();
        level();
        gold();
        gem();
    }
    private void Name()
    {
        nameText.text = DataManager.instance.playerdata.name.ToString();
    }

    private void level()
    {
        levelText.text = DataManager.instance.playerdata.level.ToString();
    }

    private void gold()
    {
        goldText.text = DataManager.instance.playerdata.gold.ToString();
    }

    private void gem()
    {
        gemText.text = DataManager.instance.playerdata.gem.ToString();
    }
}
