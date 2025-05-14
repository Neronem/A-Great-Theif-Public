using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : BaseItem
{
    // effectAudio가 존재하면 아이템 획득 시 재생
    public AudioClip effectAudio;
    // 속도 아이템 하나당 한 번만 적용되도록.
    private bool isActived = false;

    protected override void HandlePlayerCollision(GameObject player)
    {
        if (effectAudio != null)
        {
            SoundManager.PlayClip(effectAudio);
        }
    // 아이템타입이 Heal이면 PlayerHealth의 Heal 발동하고 파괴
    if (itemData.Type == ItemType.Heal && player != null)
        {
            var pc = player.GetComponent<PlayerHealth>();
            pc?.Heal(itemData.Effect);
            Destroy(gameObject);
        }
        // 아니고 Speed이고 아이템 효과가 적용되지 않았으면
        else if(itemData.Type == ItemType.Speed && player != null && !isActived)
        {
            var pc = player.GetComponent<PlayerMovement>();
            var status = player.GetComponent<PlayerStatusEffects>();
            
            // 꼭 pc일 이유는 없음. status, GameManager 등 다 가능. item은 개별 코루틴이 적용되어 이전 코루틴을 기억하지 못해서 불가능.
            // 스피드 아이템 중복획득 시 지속시간만 갱신하기 위한 로직. (버프 적용중이고, 코루틴이 존재하면 = 현재 적용 중이면)
            if (pc.isSpeedBuffRunning && pc.speedBuffCoroutine != null)
            {
                //현재 적용중인 코루틴을 멈추고 원래 속도로 초기화 후 현재 적용중인 코루틴을 null로
                pc.StopCoroutine(pc.speedBuffCoroutine);
                GameManager.Instance.speed -= pc.speedBuffAmount;
                pc.speedBuffCoroutine = null;
            }
            
            // 초기 혹은 중첩 아이템 획득 시 속도에 따른 값으로 코루틴 실행하고 해당 값 저장(15 전후로 변할 것 대비)
            float currentEffect = GameManager.Instance.speed < 15 ? itemData.Effect : -itemData.Effect;
            
            pc.speedBuffAmount = currentEffect;
            pc.speedBuffCoroutine = StartCoroutine(SpeedBuffCoroutine(pc, currentEffect, 5f));

            if (status != null)
            {
                if (status.isSuper && status.speedBuffCoroutine != null)
                {
                    status.StopCoroutine(status.speedBuffCoroutine);
                    status.speedBuffCoroutine = null;
                }
                status.speedBuffCoroutine = StartCoroutine(status.SuperRoutine());
            }
        }
    }
    
    private IEnumerator SpeedBuffCoroutine(PlayerMovement pc, float effect, float duration)
    {
        // 제한 걸고, 아이템 지속시간동안 파괴되지 않으니 사라진 것처럼 충돌과 이미지를 제거하고 속도 증감 지속시간 적용 후 감증 후 제한 해제하고 아이템 파괴 해제
        isActived = true;
        pc.isSpeedBuffRunning = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.speed += effect;
        yield return new WaitForSeconds(duration);
        GameManager.Instance.speed -= effect;
        // 초기화
        pc.isSpeedBuffRunning = false;
        pc.speedBuffAmount = 0f; 
        pc.speedBuffCoroutine = null;
        isActived = false;
        Destroy(gameObject);
    }
}