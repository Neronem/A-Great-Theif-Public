# 📌 사운드
**🎇 Made by 양성대**

## ✔️ SoundManager.cs
- 배경음악과 여러 효과음을 게임이 실행되는 동안 간편히 재생할 수 있습니다.

- **``Awake()``**
    - 사운드매니저를 인스턴스화 해준 뒤, 씬이 이동되더라도 파괴되지 않도록 처리해주었습니다.
    - 이전에 사운드를 조절한 적이 있다면 PlayerPrefs로 불러와 초기값을 넣어주고, 배경음악이 계속해서 재생되도록 처리해주었습니다.
    
- **``Start()``**
    - ChangeBackGroundMusic()에 muslcClip을 넣어 배경음악을 재생합니다.

- **``ChangeBackGroundMusic(AudioClip clip)``**
    - 배경음악을 재생합니다.

- **``PlayClip(AudioClip clip)``**
    - 프리펩화한 soundSourcePrefab를 만들고 clip을 실행합니다. 

## ✔️ SoundSource.cs
- 배경음악과 효과음들의 실제 실행을 담당하는 스크립트입니다.

- **``Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)``**
  - 넘겨받은 클립, 볼륨, 피치로 재생할 소리를 만들어 재생합니다.
  - 클립의 전체 재생 시간이 끝나면 Disable()을 실행합니다.

- **``Disable()``**
  - 해당 클립 재생을 멈추고, 파괴하는 로직을 담당합니다.