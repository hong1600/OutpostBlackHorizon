using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedUp
{
    void SetIsSpeedUp(bool _value);
    bool GetIsSpeedUp();
}

public class SpeedUp : MonoBehaviour, ISpeedUp
{
    public bool isSpeedUp = false;

    public void SetIsSpeedUp(bool _value) 
    {
        isSpeedUp = _value; 
        Time.timeScale = isSpeedUp ? 2f : 1f;
    }

    public bool GetIsSpeedUp() {  return isSpeedUp; }
}
