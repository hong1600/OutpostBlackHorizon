using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum EVideo
{
    START,
    LOGIN
}

public class VideoSelector : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] List<VideoClip> videoClipList;

    EVideo eVideo = EVideo.START;

    private void Start()
    {
        PlayVideo(eVideo);
    }

    public void PlayVideo(EVideo _eVideo)
    {
        videoPlayer.clip = videoClipList[(int)_eVideo];
        videoPlayer.Play();
    }

    public void NextVideo(EVideo _eVideo)
    {
        eVideo = _eVideo;
        videoPlayer.clip = videoClipList[(int)_eVideo];
        videoPlayer.Play();
    }
}
