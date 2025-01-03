using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sever : MonoBehaviour
{
    [Serializable]
    public class Info
    {
        public int ID;
    }

    public Dictionary<int, Info> Dictionary = new Dictionary<int, Info>();

    string Http = "http://58.78.211.182:3000/";

    string ConnectUrl = "process/dbconnect";
    //string DisConnectUrl = "process/dbdisconnect";
    //string UserSelectUrl = "process/userselect";

    IEnumerator DBPost(string Url, string Num)
    {
        WWWForm form = new WWWForm();
        form.AddField("num", Num);

        UnityWebRequest www = UnityWebRequest.Post(Url, form);

        yield return www.SendWebRequest();

        JSONNode node = JSONNode.Parse(www.downloadHandler.text);

        for(int i = 0; i < node.Count; ++i) 
        {
            Info info = new Info();

            Dictionary.Add(i, info);
        }

        Debug.Log(node);
    }

    public void OnBtnConnect()
    {
        StartCoroutine(DBPost(Http + ConnectUrl, "dev"));
    }
}
