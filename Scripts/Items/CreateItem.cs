using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    // 랜덤 생성할 아이템들
    [SerializeField] private List<ItemData> itemDataList;
    
    // 초기에 생성하고 끝까지 유지되는 아이템의 개수
    private int spawnCount = 15;
    
    // 아이템 생성 시작위치
    private Vector3 startSpawnPosition = Vector3.zero;

    // 아이템 마지막 생성위치
    private Vector3 lastPosition;
    
    private void Start()
    {
        //아이템 처음 생성 = 초기화 시점
        lastPosition = startSpawnPosition;

        // 아이템 생성 개수만큼 아이템 생성
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnAndCreateItem(lastPosition);
        }
    }
    
    private GameObject SpawnRandomItem(Vector3 position)
    {
        // itemList의 가중치 합 구하고 랜덤값에 활용. 100을 안 쓰고 직접 다 구하는 이유는 손으로 하다 100이 안 되는 경우가 있을까봐.
        int totalWeight = itemDataList.Sum(item => item.Weight);
        int rand = Random.Range(1, totalWeight + 1);

        // rand 값과 비교할 아이템 생성 값. 
        int cumulative = 0;
        
        // 최대 itemList.Count 만큼 반복하여 아이템 1회 생성.
        // rand 값이 가중치의 합보다 크면 다음 가중치의 합과 비교하는 식.
        foreach (var item in itemDataList)
        {
            cumulative += item.Weight;
            if (rand <= cumulative)
            {
                GameObject go = Instantiate(item.Prefab, position, Quaternion.identity);
                BaseItem baseItem = go.GetComponent<BaseItem>();

                if (baseItem != null)
                {
                    baseItem.SetItemData(item);  // 아이템 데이터 설정
                }

                return go;
            }
        }
        return null;
    }
    
    
    // 아이템을 생성하고 위치를 업데이트하는 메서드 분리.
    public void SpawnAndCreateItem(Vector3 position)
    {
        GameObject item = SpawnRandomItem(position);
        if (item != null)
        {
            BaseItem baseItem = item.GetComponent<BaseItem>();
            lastPosition = baseItem.RandomCreate(lastPosition);  // 아이템 위치를 갱신
        }
    }
}