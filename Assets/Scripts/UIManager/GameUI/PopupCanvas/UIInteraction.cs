using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    [SerializeField] GameObject interactionPanel;
    [SerializeField] TextMeshProUGUI interactionText;

    public void OpenPanel(EObject _eObject)
    {
        switch (_eObject)
        {
            case EObject.BULLET:
                UpdateText("[F] ź�� �ݱ�");
                break;
            case EObject.TURRET:
                UpdateText("[F] ��ȭ�� ž��");
                break;
        }

        UIManager.instance.OpenPanel(interactionPanel, null, null, null, false);
    }

    private void UpdateText(string _desc)
    {
        interactionText.text = _desc;
    }

    public void ClosePanel()
    {
        UIManager.instance.ClosePanel();
    }

}
