# 📌 플레이어(Player)
---
## Player 스크립트에 대한 설명입니다.

### PlayerController.cs
- 플레이어 컨트롤러는 여타 다른 스크립트에서 작성된 메서드를 호출하는 역할을 합니다.
  - `Start()` : 스킨을 장착하고, hp를 리셋시키고, 코루틴을 실행시키는 등의 역할을 합니다.
  - `FixedUpdate()` : `PlayerMovement.CheckGround()`를 호출하고, `GameManager.speed`를 통해 움직임을 구현합니다.
  - `Update()` : 키를 감지하여 `PlayerMovement`의 점프/슬라이드를 실행합니다. 플레이어 사망조건에 따라 `PlayerHealth.Die()`를 호출합니다.
  - `ApplyStatsForSkin(string skinId)` : `SkinManager`에서 데이터를 가져와서 체력과 점프력 등을 세팅합니다.
  - `OnDestroy()` : 오브젝트 수명 종료 시 이벤트를 정리해줍니다.
 
### PlayerMovement.cs
- 플레이어 무브먼트는 플레이어의 움직임에 대한 로직을 담당합니다.
  - `CheckGround()` : 플레이어에게 붙은 GroundDetector가 groundLayer와 충돌 시 isGrounded를 true로 만듭니다.
  - `HandleJump()` : 유저가 점프로 세팅한 키를 누를 때마다 jumpCount(최대 2회)를 늘리고, jumpForce만큼 몸을 띄우고 애니메이션을 실행합니다.
  - `HandleSlide()` : 바닥에 있고, 유저가 슬라이드로 세팅한 키를 누르면 슬라이딩 애니메이션을 실행하고, 기존 플레이어 충돌 범위 콜라이더를 해제하고 슬라이딩 시 충돌 범위 콜라이더를 활성화시킵니다. 키를 떼면 애니메이션을 멈추고, 기존 플레이어 충돌 범위 콜라이더를 다시 활성화시킵니다.
 
### PlayerHealth
- 플레이어 체력에 대한 로직을 담당합니다.
  - `hpReset()` : 체력을 최대체력만큼 초기화 시킵니다.
  - `TakeDamage(float amount)` : 죽거나, 무적 상태이거나, 슈퍼 모드가 아닐 때 실행됩니다. `Undamageable`(무적상태) 코루틴을 실행합니다. 현재 체력을 amount만큼 감소시키고, 데미지 애니메이션을 실행합니다. 또, `CameraShake` 코루틴을 실행합니다. 만약 체력이 0보다 작거나 같으면 `Die()`메서드를 실행합니다.
  - `SetMaxHealth(float hp)` : maxHealth를 hp로 조정합니다. currentHealth를 maxHealth와 currentHealth 중 큰 것으로 조정합니다.
  - `Heal(float amount)` : currentHealth를 amount만큼 증가시킵니다. 만약 currentHealth가 maxHealth를 넘어가면 currentHealth를 maxHealth값으로 조정시킵니다.
  - `Die()` : 사망 애니메이션을 실행시키고, isDead를 true로 바꿉니다. 플레이어 오브젝트를 2초(애니메이션 실행 시간) 후 파괴합니다.
 
### PlayerCollisionHandler
- 플레이어와 장애물 간의 충돌을 처리하고, 장애물 클리어 횟수 및 업적 연동을 담당합니다.
  - `ObstacleClear()` : `obstacleCount`를 1 증가시킵니다. 업적 진행도를 `AchievementManager.ProgressRate()`를 통해 업데이트 합니다.
  - `ObstacleReset()` : `obstacleCount`를 0으로 초기화합니다.
  - `OnTriggerEnter2D(Collider2D collision)` : 충돌한 오브젝트에 `Obstacle`컴포넌트가 있는지 확인합니다. 만약 플레이어 충돌 범위 콜라이더나 슬라이딩 충돌 범위 콜라이더가 충돌했으면 `TakeDamage(10f)`를 호출합니다. 만약 플레이어와 슬라이딩 충돌 범위 콜라이더가 모두 충돌하지 않으면 `ObstacleClear()`를 호출하여 `obstacleCount`를 증가시킵니다.
 
### PlayerStatusEffect
- 플레이어의 상태 이상 및 시간에 따른 효과를 관리합니다.
  - `IEnumerator Undamageable()` : `isUndamageable = true`로 설정한 뒤 `unDamageable` 초만큼 대기, 다시 `false`로 해제해 일시적인 무적 상태를 구현합니다.
  - `IEnumerator SpeedUpRoutine()` : 플레이어가 죽을 때까지 `speedUpInterval`초마다 `GameManager.speed`를 `sppedUpAmount`만큼 증가시킵니다.
  - `IEnumerator HpDecrease()` : `healthdecreaseInterval` 초마다 `currentHealth`를 `healthdecreaseAmount`만큼 감소시킵니다. 체력이 0 이하가 되면 `Die()`메서드를 호출합니다.
  - `IEnumerator SuperRoutine()` : `isSuper = true`설정 후 `superDuration` 만큼 대기, 다시 `false`로 해제해 일시적인 무적 상태를 구현합니다.

### PlayerInputSettings
- 플레이어 입력 키를 관리합니다.

### DustParticleControl
- 달리기, 착지, 슬라이드 시 바닥에서 발생하는 더스트 파티클을 관리합니다.
  - `ReplaceParticle(ParticleSystem newDustParticle)` : 기존 `dustParticle` 오브젝트를 삭제 후 `newDustParticle` 프리팹으로 교체합니다. **미구현 상태**
  - `CreateDust()` : 현재 동작 중인 파티클을 멈추고 재생합니다.
