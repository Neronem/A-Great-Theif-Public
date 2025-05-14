using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //     [SerializeField] private UIManager uiManager;
    //     [SerializeField] private GameObject scoreItemPrefab; 
    //     
    private int startScore = 0;
    public int StartScore {get {return startScore;}}
    
    // 최고점수 
    private int bestScore = 0;
    public int BestScore { get { return bestScore; } }
    
    public static GameManager Instance;

    public static int difficulty = 1; //난이도 초기화
    public float speed; //속도 선언
    
    private readonly string scoreKey = $"BestScore{difficulty}";
    
    private bool isGameOver = false; // 게임오버 여부 판단
    
    //게임매니저 싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        Debug.Log("FrameRate : " + Application.targetFrameRate);

        int diff = difficulty;
        startScore = 0;
        bestScore = PlayerPrefs.GetInt(scoreKey, 0);
        
        //난이도에 따른 속도 차이
        switch (diff)
        {
            case 1:
                speed = 5f;
                break;
            case 2:
                speed = 10f; // 속도차이의 변화를 느끼기 위한 극단적인 세팅 나중에 조절
                break;
            case 3:
                speed = 7f;
                break;
        }
    }
    
        //uiManager.UpdateScore(0);

        // BaseItem[] items = GameObject.FindObjectsOfType<BaseItem>(); 
        // itemLastPosition = items[0].transform.position;
        // itemCount = items.Length;
        //
        // Debug.Log(itemCount);
        // Debug.Log(itemLastPosition);
        //
        // for (int i = 0; i < itemCount; i++)
        // {
        //     //장애물 마지막 위치 = i번째 장애물 위치
        //     itemLastPosition = items[i].RandomCreate(itemLastPosition);
        // }
        //     
        //     int itemCount = 0;
        //     Vector3 itemLastPosition = Vector3.zero;
        //         
        //
    

    private void Update()
    {
        if (StartScore >= 3000)
        {
            if (difficulty == 1)
            {
                if (PlayerPrefs.GetInt("Stage1Cleared", 0) == 0) // 스테이지1 클리어해 본 적 없었으면
                {
                    PlayerPrefs.SetInt("Stage1Cleared", 1); // 스테이지1클리어 상태로 세팅
                    PlayerPrefs.Save();
                }
                //게임종료
            }
            // HYJ합쳐지면 플레이어컨트롤러쪽으로 넘기기
        }
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("Start");
            }
        }
    }
    //     
    //     
    //     
    //     
    //     
    //
    #region Score
    
    public void AddScore(int score)
    {
        startScore += score;
    }

    public void SaveScore()
    {
        if (startScore > bestScore)
        {
            Debug.Log("최고 점수 갱신 성공!");
            bestScore = startScore;
            PlayerPrefs.SetInt(scoreKey, bestScore);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("최고 점수 갱신 실패!");
        }
    }

    #endregion
    //     

    public void GameOver()
    {
        if (!isGameOver)
        {
            SaveScore();
            isGameOver = true;
            GameUIManager.instance.GameUIDisappear();
            GameUIManager.instance.GameOverUIAppear();
        }
    }
    
    #region PlayerPrefsReset
    
#if UNITY_EDITOR
    [ContextMenu("초기화: PlayerPrefs Delete All")]
    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SkinManager.Instance.ResetSkinData();
        AchievementManager.Instance.ResetAchievement();
        Debug.Log("PlayerPrefs가 초기화되었습니다.");
    }

#endif
    #endregion
}
