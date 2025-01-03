using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnTimer
{
    void Timer();
    float GetMin();
    float GetSec();
    void SetSec(float _value);
    bool GetIsSpawnTime();
    void SetIsSpawnTime(bool _value);

}

public class SpawnTimer : MonoBehaviour, ISpawnTimer
{
    public float sec;
    public float min;
    public bool timerRunning;
    public bool spawnTime;

    private void OnEnable()
    {
        spawnTime = false;
        min = 0.0f;
        sec = 4f;
    }

    public void Timer()
    {
        if (Shared.gameMng.iRound.GetIsBossRound())
        {
            sec = 0f;
        }
        else
        {
            sec -= Time.deltaTime;

            if(sec < 4) 
            {
                int intSec = (int)sec;

                Shared.gameUI.iUISpawnPointTimerPanel.GetSpawnPointTimerPanel().SetActive(true);
                Shared.gameUI.iUISpawnPointTimerPanel.GetSpawnPointTimerText().text = intSec.ToString();
            }
        }

        int intsec = (int)sec;

        if (sec < 0f)
        {
            StartCoroutine(spawn());
            Shared.gameUI.iUIRoundPanel.RoundPanel();

            if (min > 0)
            {
                min -= 1;
            }
        }
    }

    IEnumerator spawn()
    {
        Shared.gameMng.iRound.SetCurRound(1);
        Shared.gameMng.iRound.RoundCheck();
        sec = 20f;
        spawnTime = true;
        Shared.gameUI.iUISpawnPointTimerPanel.GetSpawnPointTimerPanel().SetActive(false);

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }


    public float GetSec() { return sec; }
    public void SetSec(float _value) { sec = _value; }
    public bool GetIsSpawnTime() { return spawnTime; }
    public void SetIsSpawnTime(bool _value) { spawnTime = _value; }
    public float GetMin() { return min; }
}
