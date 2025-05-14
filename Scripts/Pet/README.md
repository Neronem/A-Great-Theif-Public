# 📌 펫 팔로우 시스템 

`PetFollow`는 플레이어를 따라다니는 펫의 움직임과, 일정 시간마다 아이템을 전달하는 기능을 담당합니다.  
펫은 `Idle`, `GoingToGive`, `Returning` 세 가지 상태를 가지며, 각 상태에 따라 다른 행동을 수행합니다.

- `player` : 따라다닐 대상인 플레이어의 Transform  
- `followOffset` : 플레이어 기준 펫의 위치 오프셋  
- `followSpeed` : 플레이어를 따라다닐 때 속도  
- `itemData` : 생성할 아이템 정보  
- `ItemGiveInterval` : 아이템을 주는 시간 간격  
- `givingItemSpeed` : 아이템을 주고 돌아오는 동안의 이동 속도  
- `giveItemPosition` : 아이템을 주는 목표 위치  

---

### Start()

- `InvokeRepeating()`을 이용해 일정 시간 간격으로 `HandleItemPosition()`을 호출하여 아이템 지급합니다.

---

### Update()

- 현재 펫의 상태에 따라 행동 로직을 수행합니다.  
  - `Idle` 상태: 플레이어의 옆 위치로 이동  
  - `GoingToGive`: 아이템 전달 위치로 이동, 도착 시 `SpawnItem()` 호출 후 `Returning` 상태로 변경  
  - `Returning`: 다시 플레이어 옆으로 이동, 도착 시 `Idle` 상태로 변경  

---

### HandleItemPosition()

- 펫이 아이템을 주러 가기 위한 위치를 설정합니다.  
- 상태를 `GoingToGive`로 변경하여 이동을 시작합니다.

---

### SpawnItem()

- `itemData.Prefab`을 기반으로 아이템 오브젝트를 생성합니다.  
- 생성된 오브젝트에 `itemData`를 설정하여 아이템의 속성을 지정합니다.

---

### 상태 Enum

```csharp
private enum PetState { Idle, GoingToGive, Returning }
