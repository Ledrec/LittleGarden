using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Mixer")]
    public AudioMixer audioMixer;
    [Range(-80,0)]
    public float masterVolume;
    [Range(-80,0)]
    public float sfxVolume;
    [Range(-80,0)]
    public float combatVolume;
    [Range(-80, 0)]
    public float uiVolume;
    [Range(-80,0)]
    public float musicVolume;
    [Range(-80, 0)]
    public float ambientVolume;
    [Header("SFX")]
    public Pool sfxPool;
    public Pool combatAudioPool;
    public UiClip[] uiClips;
    public Pool uiAudioPool;
    [Header("Music")]
    public AudioSource musicSource;
    public MusicClip[] musicClips;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        ModifyMasterVolume(masterVolume);
        ModifyMusicVolume(musicVolume);
        ModifySFXVolume(sfxVolume);
        ModifyCombatVolume(combatVolume);
        ModifyUiVolume(uiVolume);
        ModifyAmbientVolume(ambientVolume);
        PlayMusic(MusicType.Route,true);
    }
    #region Volume
    public void ModifyMasterVolume(float _value)
    {
        masterVolume = _value;
        audioMixer.SetFloat("MasterVolume",masterVolume);

    }
    public void ModifySFXVolume(float _value)
    {
        sfxVolume = _value;
        audioMixer.SetFloat("SFXVolume", sfxVolume);

    }
    public void ModifyMusicVolume(float _value)
    {
        musicVolume = _value;
        audioMixer.SetFloat("MusicVolume", musicVolume);

    }
    public void ModifyCombatVolume(float _value)
    {
        combatVolume = _value;
        audioMixer.SetFloat("CombatVolume", combatVolume);

    }
    public void ModifyUiVolume(float _value)
    {
        uiVolume = _value;
        audioMixer.SetFloat("UiVolume", uiVolume);

    }
    public void ModifyAmbientVolume(float _value)
    {
        ambientVolume = _value;
        audioMixer.SetFloat("AmbientVolume", ambientVolume);

    }
    #endregion

    #region SFX
    public void PlaySfxClip(AudioClip _clip, Vector3 _position)
    {
        GameObject go = sfxPool.GetPooledObject();
        go.transform.position = _position;
        go.GetComponent<AudioInstance>().SetAudioClip(_clip);
    }
    public void PlayCombatClip(AudioClip _clip, Vector3 _position)
    {
        GameObject go = combatAudioPool.GetPooledObject();
        go.transform.position = _position;
        go.GetComponent<AudioInstance>().SetAudioClip(_clip);
    }
    public void PlayUiClip(UiAudioType _clip)
    {
        GameObject go = uiAudioPool.GetPooledObject();
        for(int i = 0; i < uiClips.Length; i++)
        {
            if(uiClips[i].type == _clip)
            {
                go.GetComponent<AudioInstance>().SetAudioClip(uiClips[i].clip);
                break;
            }
        }
    }
    public void PlayUiClip(AudioClip _clip)
    {
        GameObject go = uiAudioPool.GetPooledObject();
        go.GetComponent<AudioInstance>().SetAudioClip(_clip);
    }
    #endregion

    #region Music
    public void PlayMusic(MusicType _type, bool _willLoop)
    {
        musicSource.loop = _willLoop;
        for(int i =0; i<musicClips.Length; i++)
        {
            if(musicClips[i].type == _type)
            {
                musicSource.Stop();
                musicSource.clip = musicClips[i].clip;
                musicSource.Play();
                break;
            }
        }
    }
    #endregion
}
public enum MusicType
{
    Route,
    Rival,
    Gym,
    RouteEnd
}
public enum UiAudioType
{
    Tap,
    Hold
}
[System.Serializable]
public class MusicClip
{
    public AudioClip clip;
    public MusicType type;
}
[System.Serializable]
public class UiClip
{
    public AudioClip clip;
    public UiAudioType type;
}