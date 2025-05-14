using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke(); //유지되는 게임오브젝트에서 오디오클립 반복 시 Invoke 취소.
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);
        _audioSource.Play();

        Invoke("Disable", clip.length);
    }

    // 해당 clip 재생 끝나면 멈추고 파괴
    public void Disable()
    {
        _audioSource.Stop();
        Destroy(gameObject);
    }
}
