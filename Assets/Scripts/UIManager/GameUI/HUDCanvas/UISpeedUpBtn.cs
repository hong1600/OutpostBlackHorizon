using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpeedUpBtn : MonoBehaviour
{
    public TextMeshProUGUI speedText;

    public void SpeedUpBtn()
    {
        if (Shared.gameMng.iSpeedUp.GetIsSpeedUp())
        {
            Shared.gameMng.iSpeedUp.SetIsSpeedUp(false);
            speedText.text = "X1";
        }
        else
        {
            Shared.gameMng.iSpeedUp.SetIsSpeedUp(true);
            speedText.text = "X2";
        }
    }
}
