using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedUp
{
    void setIsSpeedUp(bool value);
    bool getIsSpeedUp();
}

public class SpeedUp : MonoBehaviour, ISpeedUp
{
    public bool isSpeedUp = false;

    public void setIsSpeedUp(bool value) 
    {
        isSpeedUp = value; 
        Time.timeScale = isSpeedUp ? 2f : 1f;
    }
    public bool getIsSpeedUp() {  return isSpeedUp; }
}
