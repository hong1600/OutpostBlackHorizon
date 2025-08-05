using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] CustomDropdown dropdown;

    [SerializeField] TextMeshProUGUI labelText;

    Resolution[] allRes;
    List<Resolution> filterResList = new List<Resolution>();

    List<Vector2Int> commonResolutionList = new List<Vector2Int>
    {
        new Vector2Int(1280, 720),   // HD 720p
        new Vector2Int(1920, 1080) // FHD
    };

    int curIndex = 0;

    private void Start()
    {
        allRes = Screen.resolutions;

        labelText.text = Screen.currentResolution.width + " X " + Screen.currentResolution.height;

        dropdown.onOptionSelectedEvent.AddListener(SetResolution);

        List<Vector2Int> uniqueResList = new List<Vector2Int>();

        for(int i = 0; i < allRes.Length; i++) 
        {
            Vector2Int res = new Vector2Int(allRes[i].width, allRes[i].height);

            if(commonResolutionList.Contains(res) && !uniqueResList.Contains(res)) 
            {
                uniqueResList.Add(res);
                
                filterResList.Add(allRes[i]);
            }
        }


        for (int i = 0; i < filterResList.Count; i++)
        {
            Debug.Log(filterResList[i]);
        }
    }

    public void SetResolution(int _index)
    {
        curIndex = _index;
        Resolution res = filterResList[_index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        Debug.Log($"[SetResolution] index: {_index}, size: {res.width}x{res.height}, fullscreen: {Screen.fullScreen}");
    }

    public void ReapplyCurrentResolution()
    {
        if (filterResList.Count == 0) return;

        Resolution curRes = Screen.currentResolution;

        Resolution res = filterResList[curIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
