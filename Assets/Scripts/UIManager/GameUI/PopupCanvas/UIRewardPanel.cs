using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardPanel : MonoBehaviour
{
    Rewarder rewarder;

    [Header("Finish")]
    [SerializeField] GameObject finishPanel;
    [SerializeField] Image BackGround;
    [SerializeField] RectTransform finishBackRect;
    [SerializeField] RectTransform finishPanelRect1;
    [SerializeField] RectTransform finishPanelRect2;
    [SerializeField] GameObject finishBack;
    [SerializeField] GameObject finishPanel1;
    [SerializeField] GameObject finishPanel2;
    [SerializeField] TextMeshProUGUI finishText1;
    [SerializeField] TextMeshProUGUI finishText2;
    [SerializeField] GameObject finishImg;
    [SerializeField] Transform movePos1;
    [SerializeField] Transform movePos2;

    [Header("Reward")]
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] GameObject rewardPanel;

    bool isOneshot = false;

    private void Start()
    {
        rewarder = GameManager.instance.Rewarder;
        GameManager.instance.GameState.onGameFinish += UpdateRewardPanel;
    }

    IEnumerator StartAnim()
    {
        if (isOneshot) yield break;

        isOneshot = true;

        finishBackRect.DOSizeDelta(new Vector2(finishBackRect.sizeDelta.x, 400), 0.5f)
            .SetEase(Ease.OutSine);
        finishPanelRect2.DOSizeDelta (new Vector2(finishPanelRect2.sizeDelta.x, 15), 0.5f)
            .SetEase(Ease.OutSine);

        yield return new WaitForSeconds(0.15f);

        finishText2.DOFade(0,0.03f).SetEase(Ease.InOutSine).SetLoops(5, LoopType.Yoyo)
            .OnComplete(() => finishText2.alpha = 1);

        yield return new WaitForSeconds(1f);

        finishBackRect.DOSizeDelta(new Vector2(finishBackRect.sizeDelta.x, 0), 0.5f)
            .SetEase(Ease.OutSine);
        yield return finishPanelRect2.DOSizeDelta(new Vector2(finishPanelRect2.sizeDelta.x, 90), 0.5f)
            .SetEase(Ease.OutSine).WaitForCompletion();

        finishText2.enabled = false;
        finishImg.SetActive(true);
        finishPanelRect1.DOSizeDelta(new Vector2(150, finishPanelRect1.sizeDelta.y), 0.5f)
            .SetEase(Ease.OutSine);
        yield return finishPanelRect2.DOSizeDelta(new Vector2(150, finishPanelRect1.sizeDelta.y), 0.5f)
            .SetEase(Ease.OutSine).WaitForCompletion();

        yield return finishPanel1.transform.DOMove(movePos1.position, 0.5f)
            .SetEase(Ease.Linear).WaitForCompletion();

        yield return new WaitForSeconds(0.5f);

        finishPanelRect1.DOSizeDelta(new Vector2(790, finishPanelRect1.sizeDelta.y), 0.5f)
            .SetEase(Ease.Linear);
        rewardPanel.SetActive(true);
        yield return finishPanel2.transform.DOMove(movePos2.position, 0.5f)
            .SetEase(Ease.Linear).WaitForCompletion();

        finishText2.enabled = true;

        yield return new WaitForSeconds(5f);

        MSceneManager.Instance.ChangeScene(EScene.LOBBY);
    }

    public void UpdateRewardPanel(EGameState _state)
    {
        GameManager.instance.ViewState.SwitchNone();

        if (DataManager.instance.UserDataLoader.curUserData != null)
        {
            userNameText.text = DataManager.instance.UserDataLoader.curUserData.userName;
        }
        else
        {
            userNameText.text = "";
        }

        rewardText.text = GameManager.instance.Rewarder.GetReward(EReward.GOLD).ToString();

        if (_state == EGameState.GAMECLEAR)
        {
            finishText1.text = "���� ����";
            stateText.text = "����";
        }
        else if (_state == EGameState.GAMEOVER)
        {
            finishText1.text = "���� ����";
            stateText.text = "����";
        }

        finishPanel.SetActive(true);
        StartCoroutine(StartAnim());
    }
}
