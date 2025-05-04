using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    [SerializeField] GameObject interactionPanel;

    public void OpenPanel()
    {
        UIManager.instance.OpenPanel(interactionPanel, null, null, null, false);
    }

    public void ClosePanel()
    {
        UIManager.instance.ClosePanel();
    }

}
