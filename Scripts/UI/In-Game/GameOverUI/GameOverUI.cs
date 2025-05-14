using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText; // 최종 점수
    public TextMeshProUGUI bestScoreText; // 최고 점수
    public GameObject newStageOpenText; // 스테이지 오픈 시 출력되는 텍스트

    public void GameOverUIAppear()
    {
        totalScoreText.text = GameManager.Instance.StartScore.ToString();
        bestScoreText.text = GameManager.Instance.BestScore.ToString();

        if (GameManager.difficulty == 1 && PlayerPrefs.GetInt("Stage1Cleared", 0) == 1 && PlayerPrefs.GetInt("Stage1ClearedTextShowed", 0) == 0)
        { // 조건 1. 1단계일것 , 조건 2. 스테이지를 클리어한 상태일것 , 조건 3. 스테이지 클리어 문구를 한번도 못본 상태일 것
            newStageOpenText.SetActive(true);
            PlayerPrefs.SetInt("Stage1ClearedTextShowed", 1);
        }
        else
        {
            newStageOpenText.SetActive(false);
        } 
        
        gameObject.SetActive(true);
    }
}

