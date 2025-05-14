using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Player Colliders")]
    [SerializeField] private Collider2D playerCollider;   // ���� �ݶ��̴�
    [SerializeField] private Collider2D slidingCollider;  // �����̵� �ݶ��̴�
    [SerializeField] private Collider2D obstacleDetecter; // ��ֹ� ������

    private PlayerHealth health;
    private PlayerStatusEffects status;
    private int obstacleCount; // ���� ��ֹ� ��


    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        status = GetComponent<PlayerStatusEffects>();

    }
    public void ObstacleClear()
    {
        if (health.isDead) return;
        obstacleCount++;
        AchievementManager.Instance.ProgressRate("clear_50", 1);
        AchievementManager.Instance.ProgressRate("clear_100", 1);
        AchievementManager.Instance.ProgressRate("clear_200", 1);
        Debug.Log("Obstacle Clear: " + obstacleCount);
    }

    public void ObstacleReset()
    {
        if (health.isDead) return;
        obstacleCount = 0;
        Debug.Log("Obstacle Reset: " + obstacleCount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (!obstacle)
            return;

        bool playerHit = playerCollider.IsTouching(collision);
        bool slidingHit = slidingCollider.IsTouching(collision);

        if (playerHit || slidingHit)
        {
            health.TakeDamage(10f); // ��ֹ��� ����� �� ������ ó��
            return;
        }
        if (!playerHit && !slidingHit)
        {
            ObstacleClear();
        }
    }
}