# YJ_Town_Project
# 📌 스킨 데이터
**🎇 Documented By 임지우**


`SkinData`는 스킨의 외형, 능력치, 애니메이션 등을 정의하는 ScriptableObject로, 게임 내에서 다양한 스킨을 설정하고 사용할 수 있게 합니다.

- `skinId` : 스킨 고유 ID  
- `skinName` : 스킨 이름  
- `skinPrefab` : 스킨으로 사용될 프리팹  
- `defaultUnlocked` : 기본 해금 여부  
- `isUnlocked` : 현재 해금 여부  
- `maxHealth` : 해당 스킨 사용 시 체력  
- `jumpForce` : 해당 스킨 사용 시 점프력  
- `animatorOverride` : Animator Override Controller


# 📌 스킨 매니저

`SkinManager`는 모든 스킨의 상태(해금, 선택 등)를 관리하며, 게임 내 캐릭터에게 스킨을 적용하는 역할을 담당합니다.

---

### Awake()

- 싱글톤 인스턴스를 설정하고 중복 인스턴스를 제거합니다.  
- 스킨 데이터를 딕셔너리에 변환하고, 저장된 정보로 해금 상태를 불러옵니다.  
- 마지막으로 선택된 스킨 ID를 불러옵니다.

---

### UnlockSkin(string skinId)

- 특정 스킨을 해금합니다.  
- 해금되면 PlayerPrefs에 저장하고, 이벤트를 발생시킵니다.

---

### SelectSkin(string skinId)

- 선택한 스킨이 해금된 상태일 경우 저장하고 이벤트를 발생시킵니다.  

---

### CheckSkinUnlocked(string skinId)

- 해당 스킨이 해금되어 있는지 확인합니다.

---

### ApplySkin(GameObject player, string skinId)

- 특정 스킨의 프리팹을 `player` 객체의 `SkinHolder`에 적용합니다.  
- Animator 및 Animator Override도 적용됩니다.  

---

### GetSkinData(string skinId)

- 해당 ID의 스킨 데이터를 반환합니다.  

---

### OnPlayerSpawn(GameObject player)

- 저장된 스킨 ID가 있을 경우, 해당 스킨을 플레이어에 적용합니다.

---

### ResetSkinData()

- 모든 스킨을 기본 해금 상태로 초기화합니다.  
- PlayerPrefs 정보도 같이 초기화됩니다.
