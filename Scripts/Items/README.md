# 📌 아이템
**🎇 Made by 양성대**

## ✔️ BaseItem.cs
- BaseItem은 각 아이템이라면 가져야 할 기본 값들과 충돌 시 발생 효과 및 아이템 생성 후 위치 조절 기능을 담당합니다.  

- **``OnTriggerEnter2D(Collider2D collision)``**
    - 아이템이 Player 혹은 BgLooper 태그와 충돌할 때의 로직과 연결되어 있습니다.
    - 한 아이템당 한 번씩 작동하도록 변수를 만들어 조절하며 Player와 충돌 시 아이템 별 플레이어에게 다른 효과를 부여하고 해당 아이템을 파괴한 뒤, 새 오브젝트를 재생성합니다.
    - BgLooper와 충돌 시, 효과처리 없이 새로운 아이템을 생성한 뒤 해당 아이템을 파괴합니다. 
    
- **``HandlePlayerCollision(GameObject player)``**
    - BaseItem을 상속받은 클래스들에서 플레이어에게 virtual로 만들어 실제 효과를 부여할 때 사용합니다.

- **``RandomCreate(Vector3 lastPosition)``**
    - 아이템을 x,y값에 따라 생성하다, 생성 위치에서 0.4f 반경 안에 Obstacle이 존재하면 y축 위치를 조절합니다.
    - 해당 멤서드는 아이템이 아이템이 피해없이 획득할 수 있는지 1차적으로 검사합니다.

- **``SetItemData(ItemData data)``**
    - 아이템을 생성할 때, 존재하는 데이터를 넣습니다.

- **``OnTriggerStay2D(Collider2D collision)``**
    - RandomCreate(Vector3 lastPosition)를 통해 1차적으로 위치가 옮겨진 아이템이 또다시 장애물과 충돌하는지 여부를 검사하여 위치 조절 실행 여부를 판단합니다.
  
- **``AdjustYPositionStepByStep()``**
    - OnTriggerStay2D(Collider2D collision)를 통해 y축 위치를 조절하는 실질적인 메서드입니다.
    - 일차적으로 조절된 아이템과 장애물의 충돌여부를 판단하여 최종적으로 해당하는 y축에 아이템을 위치시킵니다.

## ✔️ CreateItem.cs
- CreateItem은 해당 아이템의 생성을 담당합니다.

- **``SpawnRandomItem(Vector3 position)``** 
    - 랜덤하게 아이템을 생성하는 로직입니다. switch문이 너무 길다고 생각하여 가중치를 이용하는 방식으로 생성합니다. 
    - 각 ItemData별 가중치 값을 전부 더한 뒤, 그 합보다 작은 랜덤값을 뽑아서 해당 값에 따라 아이템을 생성하고 데이터를 담아내는 로직입니다.

- **``SpawnAndCreateItem(Vector3 position)``**
    - 아이템을 생성한 후 생성 위치를 반환하여 마지막 위치에서부터 아이템을 생성합니다.

## ✔️ ScoreItem.cs
- ScoreItem은 BaseItem을 상속받아 점수를 획득합니다.

- **``HandlePlayerCollision(GameObject player)``**
  - ScoreItem과 충돌 시, 설정한 scoreAudio를 재생한 후 게임매니저의 AddScore에 점수를 보내어 계속해서 더해준 뒤, 해당 아이템을 파괴합니다.

## ✔️ EffectItem.cs
- EffectItem은 BaseItem을 상속받아 캐릭터에게 효과를 적용합니다.

- **``HandlePlayerCollision(GameObject player)``**
  - 오디오 클립 존재여부를 확인해서 오디오 클립을 재생합니다.
  - 힐링아이템이라면 플레이어의 현재체력에 Effect값을 더해준 뒤 해당 아이템을 파괴합니다.
  - 스피드 아이템이라면 캐릭터의 속도에 따라 Effect만큼 가감하고, 5f동안 지속 후 원래 속도로 되돌립니다.
  - 만약 효과가 적용되는 동안 다시 획득한다면, 현재 적용중인 효과를 끝낸 뒤 다시 부여하여 시간이 갱신된듯한 효과를 부여합니다. 
