using System;
using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform player; // 따라다닐 플레이어 위치
    private Vector3 followOffset = new Vector3(-1.5f, 0.5f, 0); // 플레이어 기준으로 따라다니는 위치
    public float followSpeed = 10f; // 따라다니는 속도
    
    public ItemData itemData; // 주는 아이템 종류
    private Vector3 giveItemPosition = Vector3.zero; // 주는 아이템 위치
    public float ItemGiveInterval = 20f; // 아이템 지급 주기
    private float givingItemSpeed = 25f; // 아이템주러 갔다올때 속도
    
    private enum PetState { Idle, GoingToGive, Returning } // 펫 상태 enum (평소, 주러갈때, 돌아올떄)
    private PetState state = PetState.Idle; // 기본 상태로 시작

    private void Start()
    {
        InvokeRepeating(nameof(HandleItemPosition), 1f, ItemGiveInterval); // 20초마다 아이템 지급
    }

    private void Update()
    {
        if (player == null) return; 

        switch (state) // 펫 상태에 따라 움직임 다름
        {
            case PetState.Idle: // 평소
                // 항상 고정된 위치에 붙도록
                transform.position = Vector3.MoveTowards(transform.position, player.position + followOffset, followSpeed * Time.deltaTime);
                break;

            case PetState.GoingToGive: // 아이템 주러 갈때
                transform.position = Vector3.MoveTowards(transform.position, giveItemPosition, givingItemSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, giveItemPosition) < 0.01f)
                { // 아이템 주는 위치에 도착하면
                    SpawnItem(); // 아이템 생성
                    state = PetState.Returning; // 돌아가기 상태로 변경
                }
                break;

            case PetState.Returning: // 주고 돌아올 때
                transform.position = Vector3.MoveTowards(transform.position, player.position + followOffset, givingItemSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, player.position + followOffset) < 0.01f)
                { // 평소 위치에 도착하면
                    state = PetState.Idle; // 평소 상태로 변경
                }
                break;
        }
    }

    private void HandleItemPosition() // 아이템 위치 세팅 & 펫 상태 변경
    {
        if (player == null || itemData == null) return;

        giveItemPosition = player.position + new Vector3(12.5f, 0f, 0f); // 아이템 생성 위치
        state = PetState.GoingToGive; // 펫에게 주러가라고 명령
    }

    private void SpawnItem() // 아이템 생성
    {
        GameObject go = Instantiate(itemData.Prefab, giveItemPosition, Quaternion.identity); //아이템 지정위치에 생성 
        BaseItem baseItem = go.GetComponent<BaseItem>(); 
        baseItem?.SetItemData(itemData); // 베이스아이템에 데이터 넣기 (베이스아이템 참고)
    }
}
