using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private NetworkingObject netObj;

    public AudioSource BGM;
    public GameObject SFXObject;

    public AudioMixer mixer;

    private void Awake()
    {
        instance = this;
        if (!netObj.isMine)
            Destroy(this);
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// SFX 사운드 조절
    /// </summary>
    public void SetSFXLevel(float level)
    {
        mixer.SetFloat("SFX", level);
    }
    
    /// <summary>
    /// 2023.1.1 / LJ
    /// BGM 사운드 조절
    /// </summary>
    public void SetBGMLevel(float level)
    {
        mixer.SetFloat("BGM", level);
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// SFX 사운드 재생
    /// </summary>
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject sfx = GameObject.Instantiate(SFXObject, transform);
        sfx.name = $"SFX - {sfxName}";
        sfx.TryGetComponent<AudioSource>(out AudioSource audiosource);
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(sfx, clip.length);
    }
    
    /// <summary>
    /// 2023.1.1 / LJ
    /// BGM 사운드 재생
    /// </summary>
    public void BGMPlay(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }
}
