# 📌 장애물(Obstacle)

# ObstacleManager.cs

장애물의 생성을 담당합니다

Awake
싱글톤

Start
초기 장애물(발판) 생성

LoadObstacles
장애물 프리팹 리스트 저장

CreateObstacle
장애물 랜덤 생성



# Obstacle.cs

장애물의 효과와 충돌을 담당합니다

Awake
각종 컴포넌트 호출

OnTriggerEnter2D
플레이어와 충돌 확인

BreakAndFly
파괴 파티클 생성

FlyAndDestroy
파괴 애니메이션 생성 밑 장애물 파괴