using System;
using System.Collections;
using UnityEngine;

// 이거 해놓으면 쓸까??
public enum ItemType
{
    Bronze,
    Silver,
    Gold,
    Heal,
    Speed,
}
[Serializable]
public class ItemData
{
    [SerializeField] private ItemType type;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int weight;
    [SerializeField] private int score;
    [SerializeField] private int effect;

    public ItemType Type => type; // 생성할 아이템 enum
    public GameObject Prefab => prefab; //생성할 프리펩
    public int Weight => weight; // 확률
    public int Score => score; // 점수
    public int Effect => effect; // 체력 or 속도 효과
}
public abstract class BaseItem : MonoBehaviour
{
    //Item 하나 당 포함되는 데이터.
    protected ItemData itemData;
    
    //item 별 x축 거리
    float itemSpacing = 1.5f;

    private bool isAdjusting = false;
    
    // Player랑 충돌 시 처리는 각 아이템별로.
    
    private void OnTriggerEnter2D(Collider2D collision)
    // protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 한 아이템에 해당 메서드가 여러 번 실행되는 경우가 있어 방지용
        if (isAdjusting) return;
        
        // 플레이어와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            // 메서드 중복실행 방지하고, Score/Effect Item별 효과 처리 및 Item 재생성 
            isAdjusting = true;
            
            HandlePlayerCollision(collision.gameObject);  // 자식이 override 가능
            CreateItem createItem = FindObjectOfType<CreateItem>();
            createItem?.SpawnAndCreateItem(transform.position); 
        }
        // 아이템 못 먹고 놓치면 해당 아이템 파괴하고 새 아이템 먹을 수 있게 생성  
        else if (collision.gameObject.CompareTag("BgLooper"))
        {
            isAdjusting = true;
            CreateItem createItem = FindObjectOfType<CreateItem>();
            createItem?.SpawnAndCreateItem(transform.position);

            Destroy(gameObject);
        }
    }
    
    // 아이템 별 각 효과 실행할 메서드
    protected virtual void HandlePlayerCollision(GameObject player)
    {
        
    }
    
 // 아이템 장애물 라인에 따라 생성하도록
    public Vector3 RandomCreate(Vector3 lastPosition)
    {
        // -3f = 기본위치. (바닥라인)
        float randomY = -3.5f;
        
        // x 마지막 위치에서 x축만큼 더하고, y는 바닥라인에서 생성.
        Vector3 placePosition = lastPosition + new Vector3(itemSpacing, 0);
        placePosition.y = randomY;
        transform.position = placePosition;
        
        Collider2D hit = Physics2D.OverlapCircle(placePosition, 0.4f, LayerMask.GetMask("Obstacle"));
        if (hit != null)
        {
            // 충돌 시 y 위치 변경 (ex. 위로 띄우기)
            placePosition.y = -2f;
            transform.position = placePosition;
        }
        
        //다음 item position을 위해 해당 positon 반환
        return placePosition;
    }
    
    // 아이템 생성될 때, 해당 data를 각 아이템에 넣음
    public void SetItemData(ItemData data)
    {
        itemData = data;
    }
    
    // 게임 시작 ~ 종료까지, 아이템(해당 스크립트)과 장애물(Layer)이 충돌하는지 계속해서 추적하다 충돌하면 y값 이동 
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 아이템과 Obstacle Layer가 충돌하면 아이템의 y값을 올리는 메서드 호출. isAdjusting = 반복 제거용
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") && !isAdjusting)
        {
            // 코루틴으로 처리하는 이유 : 일반 메서드로 진행할 경우, y값 이동하는 도중 다시 호출되어, -2f 위치에서 충돌이 나지 않는데도 
            // -1f로 이동될 수 있어, 코루틴으로 처리 후 -2f로 이동 후 다시 충돌 여부 확인하여 -1f로 이동하기 위함.
            StartCoroutine(AdjustYPositionStepByStep());
        }
    }
    
    IEnumerator AdjustYPositionStepByStep()
    {
        isAdjusting = true;
    
        float[] yLevels = { -2f, -1f}; // 처음엔 -2f, 한 번 더 검사해서 충돌하면 -1f로 이동 예정.
        foreach (float targetY in yLevels)
        {
            // 위치 이동
            Vector3 newPosition = transform.position;
            newPosition.y = targetY;
            transform.position = newPosition;
    
            // FixedUpdate 후 충돌 확인
            yield return new WaitForFixedUpdate();
            
            // Physics2D.OverlapCircle = trnasform.position 위치에서 radius 반경 안에 Obstacle 레이어와 충돌 여부 확인.
            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.3f, LayerMask.GetMask("Obstacle", "Ground"));
            if (hit == null)
                break; // 충돌이 없으면 멈춤
        }
    
        isAdjusting = false;
    }
}
