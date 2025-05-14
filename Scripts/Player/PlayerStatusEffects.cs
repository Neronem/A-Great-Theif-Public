using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    public float unDamageable = 1f;
    public bool isUndamageable;

    public bool isSuper = false; 
    public float superDuration = 5f;

    public float speedUpInterval = 2f;
    public float speedUpAmount = 3f;

    [SerializeField] private float healthdecreaseAmount = 0.1f; 
    [SerializeField] private float healthdecreaseInterval = 0.1f; 

    private PlayerMovement movement;
    private PlayerHealth health;
    public GameManager gameManager;
    
    public Coroutine speedBuffCoroutine;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

    }

    public IEnumerator Undamageable()
    {
        isUndamageable = true;
        yield return new WaitForSeconds(unDamageable);
        isUndamageable = false;
    }

    public IEnumerator SpeedUpRoutine()
    {
        while (!health.isDead)
        {
            yield return new WaitForSeconds(speedUpInterval);
            gameManager.speed += speedUpAmount;
            Debug.Log("Speed Up: " + gameManager.speed);
        }
    }
    public IEnumerator HpDecrease()
    {
        while (!health.isDead)
        {
            yield return new WaitForSeconds(healthdecreaseInterval);
            health.currentHealth -= healthdecreaseAmount;
            if (health.currentHealth <= 0)
                health.Die();
        }
    }
    public IEnumerator SuperRoutine()
    {
        isSuper = true;

        yield return new WaitForSeconds(superDuration);

        isSuper = false;
        speedBuffCoroutine = null;
        StartCoroutine(Undamageable());
    }
}

