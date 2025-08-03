using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropdownOption : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    int index;
    CustomDropdown dropdown;

    public void Init(int _index, string _name, CustomDropdown _dropdown)
    {
        dropdown = _dropdown;
        index = _index;

        text.text = _name;

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        dropdown.OnOptionSelected(index);
    }
}
