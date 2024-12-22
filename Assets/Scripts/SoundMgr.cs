using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shared
{
    static public SoundMgr soundMgr;
}

public class SoundMgr : MonoBehaviour
{
    AudioSource Bgm;
    AudioSource Effect;

    private void Awake()
    {
        Shared.soundMgr = this;

        DontDestroyOnLoad(this);

        Bgm = transform.Find("Bgm").GetComponent<AudioSource>();
        Bgm.loop = true;

        Effect = transform.Find("Effect").GetComponent<AudioSource>();
    }

    public void PlayBgm(string bgm)
    {
        Object obj = Resources.Load(bgm);

        if (null == obj)
            return;

        AudioClip clip = obj as AudioClip;

        if (null == clip)
            return;

        Bgm.clip = clip;
        Bgm.Play();
    }

    public void PlayEffect(string effect)
    {
        Object obj = Resources.Load(effect);

        if (null == obj)
            return;

        AudioClip clip = obj as AudioClip;

        if (null == clip)
            return;

        Effect.PlayOneShot(clip);
    }
}
