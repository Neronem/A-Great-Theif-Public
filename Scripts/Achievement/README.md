# 📌 도전 과제(Achievement)
---
## Achievement 스크립트에 대한 설명입니다.

### AchievementData
- 각각의 도전과제를 정의하는 `ScriptableObject` 파일입니다. 에디터에서 생성하여 사용합니다.
  - `achievementId` : 도전과제 식별자
  - `achievementName` : 도전과제 이름
  - `achievementDescription` : 도전과제 내용
  - `achievementTarget` : 도전과제 목표 값
  - `achievementIcon` : 도전과제 아이콘
  - `rewards` = new List<AchievementReward>() : 도전과제 달성 보상
 
### AchievementManager
- 도전과제 진행률을 관리하고 목표 달성 시 보상 지급 및 UI 호출을 담당하는 싱글톤 매니저입니다.
  - `Awake()` : 싱글톤 인스턴스, DDO처리를 합니다. `PlayerPrefs`에서 달성 여부를 불러옵니다. 이를 딕셔너리 메모리에 저장합니다.
  - `CheckAchievement(string achievementId)` : `achievementId`를 매개로 `isAchieved`를 반환하여 확인합니다.
  - `ProgressRate(string achievementId, float progress)` : 이미 달성한 도전 과제인지 확인 후, 아니면 도전과제의 현재값에 progress를 더합니다. 만약 현재값이 타겟값보다 크거나 같으면 `isAchieved`를 true로 바꾸고 PlayerPrefs에 저장합니다. UnlockAchievement를 호출합니다.
  - `ProgressReset()` : 딕셔너리 메모리 중 현재값을 0으로 초기화합니다. 플레이어 오브젝트가 생성될 때(메인씬을 로드할 때) 호출합니다.
  - `UnlockAchievement(AchievementData data)` : AchievementData를 순회하며 어떤 리워드 타입인지 확인 후, itemId에 따라 그 보상을 실행합니다. 현재는 Skin 기능만 구현되어 있습니다. `AchievementUI.ShowAchievementText`를 통해 UI 텍스트를 변경 후 호출합니다.
  - `ResetAchievement()` : 업적에 관련한 정보를 전부 초기화합니다. GameManager의 저장정보 초기화 메서드에서 호출합니다.
 
### AchievementUI
- 도전과제 UI를 담당합니다.
  - `ShowAchievementText(string text)` : UI를 활성화하고 텍스트를 변경합니다. `ShowRoutine` 코루틴을 호출합니다.
  - `IEnumerator ShowRoutine()` : 2초동안 UI를 유지한 후 비활성화합니다.
