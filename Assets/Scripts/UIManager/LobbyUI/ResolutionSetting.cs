using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    Resolution[] allRes;
    List<Resolution> filterResList = new List<Resolution>();

    List<Vector2Int> commonResolutions = new List<Vector2Int>
    {
        new Vector2Int(2560, 1440), // QHD
        new Vector2Int(1920, 1080), // FHD
        new Vector2Int(1280, 720)   // HD 720p
    };

    private void Start()
    {
        allRes = Screen.resolutions;
        int curResIndex = 0;
        List<string> optionList = new List<string>();

        for(int i = 0; i < allRes.Length; i++) 
        {
            Vector2Int res = new Vector2Int(allRes[i].width, allRes[i].height);

            if(commonResolutions.Contains(res) && !filterResList.Contains(allRes[i])) 
            {
                filterResList.Add(allRes[i]);

                if (allRes[i].width == Screen.currentResolution.width &&
                    allRes[i].height == Screen.currentResolution.height) 
                {
                    curResIndex = filterResList.Count - 1;
                }
            }
        }
    }

    public void SetResolution(int _index)
    {
        Resolution res = filterResList[_index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
