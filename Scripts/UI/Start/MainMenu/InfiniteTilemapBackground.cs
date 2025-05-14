using UnityEngine;                   // Unity 기본 네임스페이스
using UnityEngine.Tilemaps;         // Tilemap 관련 기능 사용을 위해 필요

public class InfiniteTilemapBackground : MonoBehaviour
{
    public GameObject[] backgrounds; // 반복할 배경 타일맵 오브젝트 2개를 담을 배열 (Inspector에서 넣음)
    public float scrollSpeed = 2f;   // 배경이 왼쪽으로 움직일 속도 (초당 몇 유닛 움직일지)

    private float backgroundWidth;   // 타일맵 한 장의 실제 가로 너비 (스크롤 반복 기준)

    void Start()
    {
        // 첫 번째 타일맵에서 Tilemap 컴포넌트를 가져옴
        Tilemap tilemap = backgrounds[0].GetComponent<Tilemap>();

        // 타일맵의 너비 계산: 타일 개수(x) × 타일 1칸 크기(x)
        backgroundWidth = tilemap.size.x * tilemap.cellSize.x;
    }

    void Update() 
    {
        // 배열에 있는 각 타일맵 오브젝트를 반복
        foreach (GameObject bg in backgrounds)
        {
            // 왼쪽(-X)으로 scrollSpeed만큼 계속 이동시키기 (Time.deltaTime은 프레임 보정용)
            bg.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // 배경이 화면 왼쪽 바깥으로 완전히 나가면...
            if (bg.transform.position.x <= -backgroundWidth)
            {
                // 가장 오른쪽에 있는 배경의 X 위치를 구해서
                float rightMostX = GetRightMostX();

                // 지금 배경을 그 오른쪽 위치 다음으로 재배치
                bg.transform.position = new Vector3(rightMostX + backgroundWidth, bg.transform.position.y, bg.transform.position.z);
            }
        }
    }

    // 현재 배경들 중 가장 오른쪽에 있는 X 위치를 반환하는 함수
    float GetRightMostX()
    {
        float maxX = float.MinValue; // 매우 작은 값으로 초기화

        foreach (GameObject bg in backgrounds)
        {
            // 현재 배경의 X 위치가 더 크면 갱신
            if (bg.transform.position.x > maxX)
                maxX = bg.transform.position.x;
        }

        return maxX; // 가장 오른쪽 배경의 X 위치 반환
    }
}