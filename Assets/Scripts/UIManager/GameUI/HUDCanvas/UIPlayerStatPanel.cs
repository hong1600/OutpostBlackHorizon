using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatPanel : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;
    
    [SerializeField] Image hpValue;
    [SerializeField] Image energyValue;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI energyText;

    private void Start()
    {
        playerStat.onTakeDmg += UpdateHp;

        UpdateHp();
        UpdateEnergy();
    }

    private void UpdateHp()
    {
        hpValue.fillAmount = playerStat.curHp / playerStat.maxHp;
        hpText.text = $"{playerStat.curHp}/{playerStat.maxHp}";
    }

    private void UpdateEnergy()
    {
        energyValue.fillAmount = playerStat.curEnergy / playerStat.maxEnergy;
        energyText.text = $"{playerStat.curEnergy}/{playerStat.maxEnergy}";
    }
}
