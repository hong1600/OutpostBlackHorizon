using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;

    [SerializeField] TextMeshProUGUI masterText;
    [SerializeField] TextMeshProUGUI sfxText;
    [SerializeField] TextMeshProUGUI bgmText;

    private void Start()
    {
        masterSlider.value = AudioManager.instance.masterVolume;
        sfxSlider.value = AudioManager.instance.sfxVolume;
        bgmSlider.value = AudioManager.instance.bgmVolume;

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
    }

    private void SetMasterVolume(float _value)
    {
        AudioManager.instance.SetMasterVolume(_value);
        masterText.text = (masterSlider.value * 100).ToString("0");
    }

    private void SetSfxVolume(float _value)
    {
        AudioManager.instance.SetSfxVolume(_value);
        masterText.text = (sfxSlider.value * 100).ToString("0");
    }

    private void SetBgmVolume(float _value)
    {
        AudioManager.instance.SetBgmVolume(_value);
        masterText.text = (bgmSlider.value * 100).ToString("0");
    }
}
