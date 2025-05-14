using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;
    [SerializeField] private Slider sePitchSlider;
    [SerializeField] private Slider musicVolumeSlider;
    
    private void Start()
    {
        if (SoundManager.Instance == null) return;

        // 현재 값을 UI에 적용
        masterVolumeSlider.value = SoundManager.Instance.MasterVolume;
        seVolumeSlider.value = SoundManager.Instance.SoundEffectVolume;
        sePitchSlider.value = SoundManager.Instance.SoundEffectPitchVariance;
        musicVolumeSlider.value = SoundManager.Instance.MusicVolume;

        // 이벤트 연결 - 마스터 하나 조절 시 나머지도 전부 조절되도록.
        masterVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.MasterVolume = value;
            
            // 마스터가 다른 슬라이더도 갱신
            seVolumeSlider.SetValueWithoutNotify(value);
            sePitchSlider.SetValueWithoutNotify(value);
            musicVolumeSlider.SetValueWithoutNotify(value);

            // 실제 볼륨 값도 갱신
            SoundManager.Instance.SoundEffectVolume = value;
            SoundManager.Instance.SoundEffectPitchVariance = value;
            SoundManager.Instance.MusicVolume = value;
        });
        
        seVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SoundEffectVolume = value;
        });

        sePitchSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SoundEffectPitchVariance = value;
        });

        musicVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.MusicVolume = value;
        });
    }
}
