using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHp : MonoBehaviour
{
    [SerializeField] Image hpValue;
    [SerializeField] TextMeshProUGUI hpText;

    private void Start()
    {
        Shared.playerMng.onTakeDmg += UpdateHpBar;
        hpValue.fillAmount = Shared.playerMng.curHp / Shared.playerMng.maxHp;
    }


    private void UpdateHpBar()
    {
        hpValue.fillAmount = Shared.playerMng.curHp / Shared.playerMng.maxHp;

        hpText.text = $"{Shared.playerMng.curHp}/{Shared.playerMng.maxHp}";
    }
}
