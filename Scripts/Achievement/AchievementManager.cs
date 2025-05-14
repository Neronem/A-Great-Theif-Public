using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public enum RewardType
    {
        UnlockCustomization, // 커스터마이징 해금
        UnlockParticle, // 파티클 해금
        UnlockPet, // 펫 해금
    }
    public static AchievementManager Instance; // 싱글톤 인스턴스

    [SerializeField] private List<AchievementData> achievementDatas; // 업적 데이터 리스트
    private Dictionary<string, Achievement> achievementDictionary; // 업적 데이터 딕셔너리

    private class Achievement
    {
        public AchievementData data; // 업적 데이터
        public bool isAchieved; // 업적 달성 여부
        public float currentValue; // 현재 값
    }
    [Serializable] public class AchievementReward
    {
        public RewardType rewardType; // 보상 타입
        public int rewardValue; // 보상 값
        public string itemId; // 아이템 ID
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        achievementDictionary = new Dictionary<string, Achievement>();
        foreach (var data in achievementDatas)
        {
            bool unlocked = PlayerPrefs.GetInt($"Achieve_{data.achievementId}", 0) == 1;
            achievementDictionary[data.achievementId] = new Achievement
            {
                data = data,
                isAchieved = unlocked,
                currentValue = 0
            };
        }
    }
    public bool CheckAchievement(string achievementId) // 업적 달성 체크
    {
        if (achievementDictionary.TryGetValue(achievementId, out var achievement))
        {
            return achievement.isAchieved; // 업적 달성 여부
        }
        return false;
    }

    public void ProgressRate(string achievementId, float progress)
    {
        if (!achievementDictionary.TryGetValue(achievementId, out var achievement)) return;

        if (achievement.isAchieved)
            return; 

        achievement.currentValue += progress;
        if (achievement.currentValue >= achievement.data.achievementTarget)
        {
            achievement.isAchieved = true;
            PlayerPrefs.SetInt($"Achieve_{achievement.data.achievementId}", 1);
            PlayerPrefs.Save();

            UnlockAchievement(achievement.data);
        }
    }
    public void ProgressReset()
    {
        foreach (var kv in achievementDictionary)
        {
            kv.Value.currentValue = 0f;
        }
    }
    private void UnlockAchievement(AchievementData data)
    {
        foreach (var reward in data.rewards)
        {
            switch (reward.rewardType)
            {
                case RewardType.UnlockCustomization:
                    SkinManager.Instance.UnlockSkin(reward.itemId); // 커스터마이징 해금 로직
                    break;
                case RewardType.UnlockParticle:
                    // 파티클 해금 로직
                    break;
                case RewardType.UnlockPet:
                    // 펫 해금 로직
                    break;
            }
        }
        if (AchievementUI.Instance != null)
            AchievementUI.Instance.ShowAchievementText($"{data.achievementName} 달성!");
        else
            Debug.LogError("AchievementUI.Instance가 null입니다");
    }
    public void ResetAchievement() // 업적 초기화
    {
        foreach (var kv in achievementDictionary)
        {
            PlayerPrefs.SetInt($"Achieve_{kv.Key}", 0);
            kv.Value.isAchieved = false;
            kv.Value.currentValue = 0;
        }
        PlayerPrefs.Save();
    }
}