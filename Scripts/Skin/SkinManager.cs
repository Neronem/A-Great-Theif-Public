using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    [SerializeField] private List<SkinData> skinDatas; // 스킨 데이터 리스트
    private Dictionary<string, Skin> skinDictionary; // 스킨 데이터 딕셔너리
    private string savedSkinId;
    public string SavedSkinId => savedSkinId;
    public event Action<string> OnSkinUnlocked; // 스킨 해금 이벤트
    public event Action<string> OnSkinSelected; // 스킨 선택 이벤트
    private class Skin
    {
        public SkinData data; // 스킨 데이터
        public bool isUnlocked; // 스킨 해금 여부
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 인스턴스 설정
            DontDestroyOnLoad(gameObject); // DDO
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
            return;
        }
        skinDictionary = skinDatas.ToDictionary(
            d => d.skinId,
            d => new Skin
            {
                data = d,
                isUnlocked = PlayerPrefs.GetInt($"Skin_{d.skinId}_Unlocked", d.isUnlocked ? 1 : 0) == 1
            }
        );
        foreach (var kv in skinDictionary)
            kv.Value.data.isUnlocked = kv.Value.isUnlocked;

        savedSkinId = PlayerPrefs.GetString("SelectedSkin", null);
    }

    public void UnlockSkin(string skinId) // 스킨 해금
    {
        if (skinDictionary.TryGetValue(skinId, out var skin) && !skin.isUnlocked)
        {
            skin.isUnlocked = true;
            PlayerPrefs.SetInt($"Skin_{skinId}_Unlocked", 1);
            PlayerPrefs.Save();
            OnSkinUnlocked?.Invoke(skinId);
        }
    }

    public void SelectSkin(string skinId)
    {
        if (!skinDictionary.TryGetValue(skinId, out var skin) || !skin.isUnlocked)
        {
            return;
        }
        savedSkinId = skinId; // 선택한 스킨 저장
        PlayerPrefs.SetString("SelectedSkin", skinId);
        PlayerPrefs.Save();
        OnSkinSelected?.Invoke(skinId);
    }

    public bool CheckSkinUnlocked(string skinId) // 스킨 해금 체크
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            return skin.isUnlocked; // 스킨 해금 여부
        }
        return false;
    }

    private void ApplySkin(GameObject player, string skinId)
    {
        if (!skinDictionary.TryGetValue(skinId, out var skin)
            || !skin.isUnlocked
            || skin.data.skinPrefab == null)
            return;

        var skinHolder = player.transform.Find("SkinHolder");
        if (skinHolder == null)
        {
            Debug.LogError("Player prefab에 SkinHolder가 없습니다!");
            return;
        }

        for (int i = skinHolder.childCount - 1; i >= 0; i--)
            Destroy(skinHolder.GetChild(i).gameObject);

        var goSkin = Instantiate(skin.data.skinPrefab, skinHolder);
        goSkin.name = skin.data.skinPrefab.name;
        goSkin.transform.localPosition = Vector3.zero;
        goSkin.transform.localRotation = Quaternion.identity;

        var newAnim = goSkin.GetComponent<Animator>();
        if (newAnim != null)
        {
            var movement = player.GetComponent<PlayerMovement>();
            movement.animator = newAnim;

            var health = player.GetComponent<PlayerHealth>();
            if (health != null)
                health.AssignAnimator(newAnim);


            if (skin.data.animatorOverride != null)
                newAnim.runtimeAnimatorController = skin.data.animatorOverride;
        }
    }
    public SkinData GetSkinData(string skinId)
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
            return skin.data;
        return null;
    }
    public void OnPlayerSpawn(GameObject player)
    {
        if (string.IsNullOrEmpty(savedSkinId))
        {
            return; // 선택된 스킨이 없으면 종료
        }
        ApplySkin(player, savedSkinId); // 선택된 스킨 적용
        Debug.Log($"OnPlayerSpawn: applying {savedSkinId} to {player.name}");
    }
    public void ResetSkinData()
    {
        foreach (var kv in skinDictionary)
        {
            var skin = kv.Value;
            skin.isUnlocked = skin.data.defaultUnlocked;
            skin.data.isUnlocked = skin.isUnlocked;
            PlayerPrefs.SetInt($"Skin_{skin.data.skinId}_Unlocked", skin.isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

}
