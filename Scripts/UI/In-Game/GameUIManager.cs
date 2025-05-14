using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    // 싱글톤 선언
    public static GameUIManager instance;
    
    public GameObject gameUI;
    public GameObject gameOverUI;
    
    public GameOverUI gameOverUIScript;
    public GameUI gameUIScript;
    
    private void Awake()
    {
        // 싱글톤패턴
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameUI.SetActive(true); 
        gameOverUI.SetActive(false);
    }

    public void GameOverUIAppear()
    {
        gameOverUIScript.GameOverUIAppear();
    }

    public void GameUIDisappear()
    {
        gameUIScript.GameUIDisappear();
    }
    
    public void UpdateScore(int score)
    {
        Debug.Log($"score가 들어왔습니다. {score}");
    }
}
