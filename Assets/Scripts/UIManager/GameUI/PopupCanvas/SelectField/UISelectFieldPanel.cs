using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectFieldPanel : MonoBehaviour
{
    [SerializeField] GameObject fieldPanel;
    [SerializeField] GameObject btnPre;
    [SerializeField] List<FieldData> btnData;
    [SerializeField] Transform parent;

    private void Start()
    {
        InputManager.instance.onInputB += OpenPanel;
        CreateBtn();
    }

    private void OpenPanel()
    {
        fieldPanel.SetActive(!fieldPanel.activeSelf);
    }

    private void CreateBtn()
    {
        for (int i = 0; i < btnData.Count; i++) 
        {
            GameObject slot = Instantiate(btnPre, parent);
            UISelectFieldBtn btn = slot.GetComponent<UISelectFieldBtn>();
            btn.Init(btnData[i]);
        }
    }
}
