using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardPanel : MonoBehaviour
{
    [Header("Finish")]
    [SerializeField] Image BackGround;
    [SerializeField] RectTransform finishBackRect;
    [SerializeField] RectTransform finishPanelRect1;
    [SerializeField] RectTransform finishPanelRect2;
    [SerializeField] GameObject finishBack;
    [SerializeField] GameObject finishPanel1;
    [SerializeField] GameObject finishPanel2;
    [SerializeField] TextMeshProUGUI finishText;
    [SerializeField] GameObject finishImg;
    [SerializeField] Transform movePos1;
    [SerializeField] Transform movePos2;

    [Header("Reward")]
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] GameObject rewardPanel;

    private void Start()
    {
        Shared.gameMng.GameState.onGameFinish += UpdateRewardPanel;
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        finishBackRect.DOSizeDelta(new Vector2(finishBackRect.sizeDelta.x, 400), 0.5f)
            .SetEase(Ease.OutSine);
        yield return finishPanelRect2.DOSizeDelta (new Vector2(finishPanelRect2.sizeDelta.x, 15), 0.5f)
            .SetEase(Ease.OutSine);

        yield return new WaitForSeconds(0.15f);

        finishText.DOFade(0,0.03f).SetEase(Ease.InOutSine).SetLoops(5, LoopType.Yoyo)
            .OnComplete(() => finishText.alpha = 1);

        yield return new WaitForSeconds(1f);

        finishBackRect.DOSizeDelta(new Vector2(finishBackRect.sizeDelta.x, 0), 0.3f)
            .SetEase(Ease.OutSine);
        yield return finishPanelRect2.DOSizeDelta(new Vector2(finishPanelRect2.sizeDelta.x, 90), 0.3f)
            .SetEase(Ease.OutSine).WaitForCompletion();

        finishText.enabled = false;
        finishImg.SetActive(true);
        finishPanelRect1.DOSizeDelta(new Vector2(150, finishPanelRect1.sizeDelta.y), 0.3f)
            .SetEase(Ease.OutSine);
        yield return finishPanelRect2.DOSizeDelta(new Vector2(150, finishPanelRect1.sizeDelta.y), 0.3f)
            .SetEase(Ease.OutSine).WaitForCompletion();

        yield return finishPanel1.transform.DOMove(movePos1.position, 0.3f)
            .SetEase(Ease.Linear).WaitForCompletion();


        finishPanelRect1.DOSizeDelta(new Vector2(790, finishPanelRect1.sizeDelta.y), 0.3f)
            .SetEase(Ease.Linear);
        yield return finishPanel2.transform.DOMove(movePos2.position, 0.3f)
            .SetEase(Ease.Linear).WaitForCompletion();

        finishText.enabled = true;

        rewardPanel.SetActive(true);
    }

    //IEnumerator StartAnim()
    //{
    //    yield return new WaitForSeconds(1.1f);

    //    finishText.SetActive(false);

    //    yield return new WaitForSeconds(0.2f);

    //    finishImg.SetActive(true);

    //    finishPanel1.transform.DOMove(movePos1.position, 1).SetEase(Ease.InSine);

    //    yield return new WaitForSeconds(1.4f);

    //    finishPanel2.transform.DOMove(movePos2.position, 0.33f).SetEase(Ease.InSine);

    //    yield return new WaitForSeconds(0.4f);

    //    rewardPanel.SetActive(true);
    //    finishText.SetActive(true);

    //    yield return new WaitForSeconds(2);

    //    yield return StartCoroutine(UIMng.instance.StartFadeIn(finishBackImg, 5));

    //    SceneMng.Instance.ChangeScene(EScene.WAITING,true);
    //}

    public void UpdateRewardPanel()
    {
        if (DataManager.instance.UserDataLoader.curUserData != null)
        {
            userNameText.text = DataManager.instance.UserDataLoader.curUserData.userName;
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
