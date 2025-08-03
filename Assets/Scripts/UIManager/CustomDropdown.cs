using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomDropdown : MonoBehaviour
{
    public UnityEvent<int> onOptionSelectedEvent;

    [SerializeField] TextMeshProUGUI labelText;

    [SerializeField] List<string> optionNameList = new List<string>();
    [SerializeField] Transform optionParent;
    [SerializeField] GameObject optionPrefab;

    bool isDropdown = false;

    private void Start()
    {
        CreateOption();

        labelText.text = "Ã¢¸ðµå";
    }

    public void ClickDropdown()
    {
        if (isDropdown)
        {
            optionParent.gameObject.SetActive(false);
            isDropdown = false;
        }
        else
        {
            optionParent.gameObject.SetActive(true);
            isDropdown = true;
        }
    }

    private void CreateOption()
    {
        float optionOffset = -50f;

        for(int i = 0; i < optionNameList.Count; i++) 
        {
            Vector3 offset = new Vector3 (0, optionOffset, 0);

            GameObject optionObj = Instantiate(optionPrefab, transform.position + offset, Quaternion.identity, optionParent);
            CustomDropdownOption option = optionObj.GetComponent<CustomDropdownOption>();
            option.Init(i, optionNameList[i], this);

            optionOffset -= 50;
        }
    }

    public void OnOptionSelected(int _index)
    {
        labelText.text = optionNameList[_index];

        optionParent.gameObject.SetActive(false);
        isDropdown = false;

        onOptionSelectedEvent?.Invoke(_index);
    }
}
