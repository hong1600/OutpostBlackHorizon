using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardPanel : MonoBehaviour
{
    [Header("Finish")]
    [SerializeField] Image finishBackImg;
    [SerializeField] GameObject finishPanel1;
    [SerializeField] GameObject finishPanel2;
    [SerializeField] GameObject finishText;
    [SerializeField] GameObject finishImg;
    [SerializeField] Transform movePos1;
    [SerializeField] Transform movePos2;

    [Header("Reward")]
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] GameObject rewardPanel;

    [Header("Anim")]
    [SerializeField] AnimationClip finishBackClip;

    private void OnEnable()
    {
        Shared.gameMng.GameState.onGameFinish += UpdateRewardPanel;
    }

    private void OnDisable()
    {
        Shared.gameMng.GameState.onGameFinish -= UpdateRewardPanel;
    }

    private void Start()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(1.1f);

        finishText.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        finishImg.SetActive(true);

        finishPanel1.transform.DOMove(movePos1.position, 1).SetEase(Ease.InSine);

        yield return new WaitForSeconds(1.4f);

        finishPanel2.transform.DOMove(movePos2.position, 0.33f).SetEase(Ease.InSine);

        yield return new WaitForSeconds(0.4f);

        rewardPanel.SetActive(true);
        finishText.SetActive(true);

        yield return new WaitForSeconds(2);

        yield return StartCoroutine(UIMng.instance.StartFadeIn(finishBackImg, 5));

        SceneMng.Instance.ChangeScene(EScene.WAITING,true);
    }

    public void UpdateRewardPanel()
    {
        if (DataMng.instance.UserDataLoader.curUserData != null)
        {
            userNameText.text = DataMng.instance.UserDataLoader.curUserData.userName;
        }
        else
        {
            userNameText.text = "";
        }

        dmgText.text = Shared.playerMng.playerCombat.dmg.ToString();

        rewardText.text = Shared.gameMng.Rewarder.GetReward(EReward.GOLD).ToString();

        if (!Shared.playerMng.playerStat.isDie)
        {
            stateText.text = "생존";
        }
        else
        {
            stateText.text = "실종";
        }
    }
}
