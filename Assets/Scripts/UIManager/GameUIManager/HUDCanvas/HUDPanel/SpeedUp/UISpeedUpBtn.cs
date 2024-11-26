using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpeedUpBtn : MonoBehaviour
{
    public SpeedUp speedUp;
    public ISpeedUp iSpeedUp;

    public TextMeshProUGUI speedText;

    private void Awake()
    {
        iSpeedUp = speedUp;
    }

    public void speedUpBtn()
    {
        if (iSpeedUp.getIsSpeedUp())
        {
            iSpeedUp.setIsSpeedUp(false);
            speedText.text = "X1";
        }
        else
        {
            iSpeedUp.setIsSpeedUp(true);
            speedText.text = "X2";
        }
    }
}
