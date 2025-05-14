using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScoreType { Current, Best } // 현재점수, 최고점수 구분용

public class ShowScore : MonoBehaviour
{
    public Sprite[] numberSprites; // 숫자 스프라이트 목록들
    public GameObject numberPrefab; // 숫자 하나 표시할때 쓰는 프리펩
    public Transform prefabParent; // 프리펩들이 붙을 부모
    
    private List<GameObject> numbers = new List<GameObject>(); // 현재 보여지고 있는 숫자들 리스트
    
    public ScoreType scoreType; // enum 인스펙터에서 할당용
    
    private void Update()
    {
        int score = 0;

        if (scoreType == ScoreType.Current) // 현재점수일땐
            score = GameManager.Instance.StartScore; // 현재점수 가져옴
        else if (scoreType == ScoreType.Best) // 최고점수일땐
            score = GameManager.Instance.BestScore; // 최고점수 가져옴

        UpdateScoreDisplay(score); // 숫자 업데이트
    }


    public void UpdateScoreDisplay(int score)
    {
        foreach (var obj in numbers)
        {
            Destroy(obj); // obj 객체들 지우기 (생성했던 숫자들을 전부 지우기)
        }
        numbers.Clear(); // obj 참조들 지우기
        
        string scoreString = score.ToString();

        foreach (char c in scoreString)
        {
            int num = c - '0'; // char 문자 -> 숫자로 변환하는 과정
            
            GameObject number = Instantiate(numberPrefab, prefabParent); // 숫자프리펩 생성 (Image 컴포넌트만 들어있음)
            number.GetComponent<Image>().sprite = numberSprites[num]; // 이미지의 Sprite를 숫자 이미지 목록에서 알맞은걸로 선택해 넣기
            numbers.Add(number); // 삭제해야할 숫자목록에 추가 (다음프레임에 삭제)
        }
    }
}
