using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum EBgm
{
    LOGIN,
    LOBBYWAITING,
    GAMESTART,
    DESERT,
    WATER,
    GAMEFINISH,
}

public enum ESfx
{
    FOOTSTEP,
    JUMP,
    GUNSHOT,
    GUNRELOAD,
    GRENADESHOT,
    EXPLOSION,
    MOSTERATTACK,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    AudioSource sfxSource;
    AudioSource bgmSource;

    [Header("Clip")]
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] sfxClips;

    [Header("Volume")]
    [Range(0f, 1f)] public float masterVolume = 1.0f;
    [Range(0f, 1f)] public float bgmVolume = 1.0f;
    [Range(0f, 1f)] public float sfxVolume = 1.0f;

    [Header("Volume")]
    [SerializeField] float minDistance = 1f;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;

        sfxSource.rolloffMode = rolloffMode;
        sfxSource.minDistance = minDistance;
        sfxSource.maxDistance = maxDistance;
        sfxSource.spatialBlend = 1f;
    }

    public void PlayBgm(EBgm _eBgm)
    {
        int bgmIndex = (int)_eBgm;

        if (bgmIndex >= 0 && bgmIndex < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[bgmIndex];
            bgmSource.volume = bgmVolume * masterVolume;
            bgmSource.Play();
        }
        else return;
    }

    public void StopBgm() 
    {
        bgmSource.Stop();
    }

    public void SetBgmVolume(float _volume)
    {
        bgmVolume = Mathf.Clamp(_volume, 0f, 1f);
        bgmSource.volume = bgmVolume * masterVolume;
    }

    public void PlaySfx(ESfx _eSfx, Vector3 _pos)
    {
        int sfxIndex = (int)_eSfx;

        if (sfxIndex >= 0 && sfxIndex < sfxClips.Length)
        {
            sfxSource.transform.position = _pos;
            sfxSource.PlayOneShot(sfxClips[sfxIndex], sfxVolume * masterVolume);
        }
        else return;
    }

    public void SetSfxVolume(float _volume)
    {
        sfxVolume = Mathf.Clamp(_volume, 0f, 1f);
        sfxSource.volume = sfxVolume * masterVolume;
    }

    public void SetMasterVolume(float _volume)
    {
        masterVolume = Mathf.Clamp(_volume, 0f, 1f);

        bgmSource.volume = bgmVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
    }
}
