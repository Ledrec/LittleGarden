using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : MonoBehaviour
{
    public AudioSource audioSource;
    public void SetAudioClip(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
        StartCoroutine(IEDisable());
    }

    IEnumerator IEDisable()
    {
        yield return null;
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
