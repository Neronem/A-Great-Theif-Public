# 📌 UI

- UI 요소를 관리하는 클래스를 담고 있는 폴더입니다.
- `StartUIManager`, `GameUIManager`로 이루어져 있습니다.

## 🔹 StartUIManager

- Start 씬의 UI를 관리하는 메소드들로 이루어져 있습니다.

  - `ShowPanel()`
    - 모든 패널을 우선 전부 Off한 후, 원하는 패널만 On 상태로 설정합니다.

  - `Onclick{ButtonName}()`
    - 버튼에 입히는 메소드로, `ShowPanel()` 메소드를 이용해 원하는 패널만 키도록 만듭니다.

  - `Update()`
    - 프레임마다 실행하며, `Esc` 키가 입력될 시 바로 전 단계의 패널로 회귀하도록 만듭니다.

## 🔹 GameUIManager

- GameUI와 GameOverUI의 On/Off 상태를 조절하는 메소드들로 이루어져 있습니다.

  - `GameOverUIAppear()`
    - GameOverUI의 점수와 스테이지 클리어 문구를 알맞게 설정한 후, GameOverUI를 On 상태로 만듭니다.

  - `GameUIDisAppear()`
    - GameUI를 가려야 할 때, GameUI를 Off 상태로 만듭니다.

  - `Start()`
    - 첫 실행 시 GameUI를 On 상태로 표시하고, GameOverUI를 Off 상태로 표시합니다.
