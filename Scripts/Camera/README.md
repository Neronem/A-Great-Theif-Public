# 📌 카메라(Camera)

# Background.cs

배경의 색상 변경을 담당합니다

- 주요 변수
bg : 색상을 변경할 백그라운드 타일맵을 호출
Color ... : 색상 지정
timeCycleDuration : 색상 변경 간격

- Start
bg를 호출

- Update
time : 시간 / 간격의 나머지 값으로 0-59의 값을 지님
t : time / 간격으로 0-1의 값을 지님
currentColor : 실시간으로 변동되는 색상

if문으로 t값이 변동하면 Lerp로 색상을 그라데이션으로 변경
bg.color를 currentColor로 지정



# BgLooper.cs

배경과 장애물의 순환을 담당합니다

- 주요 변수
numOfBg : 백그라운드의 개수

- Start
obstacleManager : ObstacleManager 스크립트를 호출
obstacleLastPosition : 장애물의 마지막 위치 저장


- OnTriggerEnter2D
CompareTag로 collision확인

collision이 Obstacle이면
widthOfObstacle : collision의 size.x값 저장
pos : collision의 위치 저장

widthOfObstacle만큼 pos를 이동
pos를 기준으로 obstacle생성


- OnTriggerExit2D
CompareTag로 collision확인

collision이 BackGround면
widthOfObstacle : collision의 size.x값 저장
pos : collision의 위치 저장
pos값으로 collision 이동

collision이 Obstacle이면
collsion파괴



# CameraShake.cs

카메라 진동 효과를 담당합니다

- 주요 변수
isShaking : 흔들리는지 판단

- Shake
코루틴, duration과 magnitude를 받아와 실행

카메라의 원래 위치 저장
elapsed : 진동 시간
duration 동안 x와 y를 랜덤값으로 움직이기

이후 본래의 위치로 복귀



# FollowCamera.cs

카메라 이동을 담당합니다

-주요 변수
target : 따라갈 오브젝트
offsetX : target과의 거리

-Start
타겟 지정 밑 초기 위치 지정

-LateUpdate
타겟 추적 이동