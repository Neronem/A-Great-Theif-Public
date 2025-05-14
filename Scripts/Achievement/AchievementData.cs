using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AchievementManager;

[CreateAssetMenu(fileName = "AchievementData", menuName = "ScriptableObjects/AchievementData", order = 1)]
public class AchievementData : ScriptableObject
{
    public string achievementId; // 업적 식별자
    public string achievementName; // 업적 이름
    public string achievementDescription; // 업적 설명
    public float achievementTarget; // 업적 목표 값
    public Sprite achievemetIcon; // 업적 아이콘
    public List<AchievementReward> rewards = new List<AchievementReward>();
}