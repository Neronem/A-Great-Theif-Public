using UnityEngine;


public class ScoreItem : BaseItem
{
    // ScoreItem 획득 시 오디오클립이 존재하면 재생
    public AudioClip scoreAudio;
    protected override void HandlePlayerCollision(GameObject player)
    {
        if (scoreAudio != null)
            SoundManager.PlayClip(scoreAudio);
        GameManager.Instance.AddScore(itemData.Score); // itemData에서 지정.
        Destroy(gameObject); // 습득한 아이템 파괴
    }
}