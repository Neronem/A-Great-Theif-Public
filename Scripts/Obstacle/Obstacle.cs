using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    public GameObject breakEffect;
    public GameObject dustEffect;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatusEffects status = collision.gameObject.GetComponent<PlayerStatusEffects>();
        if (status != null && status.isSuper)
        {

            BreakAndFly();
        }
    }

    void BreakAndFly()
    {
        Instantiate(breakEffect, transform.position, Quaternion.identity);
        Instantiate(dustEffect, transform.position, Quaternion.identity);
        
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(-1f, 1f) * 300f);
        col.enabled = false;

        // 날아가면서 사라지게
        StartCoroutine(FlyAndDestroy());
    }

    IEnumerator FlyAndDestroy()
    {
        float duration = 1f;
        float rotateSpeed = 1000f; // 초당 1000도 회전
        Vector3 flyDirection = new Vector3(-1f, Random.Range(-1.5f,2f), 0f).normalized * 1f;
        float moveSpeed = 3f;

        while (duration > 0)
        {
            duration -= Time.deltaTime;

            // 돌면서
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

            // 뒤로 날아감
            transform.position += flyDirection * moveSpeed * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}