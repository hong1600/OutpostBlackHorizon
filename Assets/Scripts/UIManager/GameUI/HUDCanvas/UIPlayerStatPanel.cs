using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatPanel : MonoBehaviour
{
    PlayerStatus playerStatus;
    
    [SerializeField] Image hpValue;
    [SerializeField] Image energyValue;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI energyText;

    private void Start()
    {
        playerStatus = Shared.playerManager.playerStatus;
        playerStatus.onTakeDmg += UpdateHp;

        UpdateHp();
        UpdateEnergy();
    }

    private void UpdateHp()
    {
        hpValue.fillAmount = playerStatus.curHp / playerStatus.maxHp;
        hpText.text = $"{playerStatus.curHp}/{playerStatus.maxHp}";
    }

    private void UpdateEnergy()
    {
        energyValue.fillAmount = playerStatus.curEnergy / playerStatus.maxEnergy;
        energyText.text = $"{playerStatus.curEnergy}/{playerStatus.maxEnergy}";
    }
}
