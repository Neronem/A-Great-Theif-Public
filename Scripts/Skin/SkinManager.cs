using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    [SerializeField] private List<SkinData> skinDatas; // ��Ų ������ ����Ʈ
    private Dictionary<string, Skin> skinDictionary; // ��Ų ������ ��ųʸ�
    private string savedSkinId;
    public string SavedSkinId => savedSkinId;
    public event Action<string> OnSkinUnlocked; // ��Ų �ر� �̺�Ʈ
    public event Action<string> OnSkinSelected; // ��Ų ���� �̺�Ʈ
    private class Skin
    {
        public SkinData data; // ��Ų ������
        public bool isUnlocked; // ��Ų �ر� ����
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // �̱��� �ν��Ͻ� ����
            DontDestroyOnLoad(gameObject); // DDO
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
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

    public void UnlockSkin(string skinId) // ��Ų �ر�
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
        savedSkinId = skinId; // ������ ��Ų ����
        PlayerPrefs.SetString("SelectedSkin", skinId);
        PlayerPrefs.Save();
        OnSkinSelected?.Invoke(skinId);
    }

    public bool CheckSkinUnlocked(string skinId) // ��Ų �ر� üũ
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            return skin.isUnlocked; // ��Ų �ر� ����
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
            Debug.LogError("Player prefab�� SkinHolder�� �����ϴ�!");
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
            return; // ���õ� ��Ų�� ������ ����
        }
        ApplySkin(player, savedSkinId); // ���õ� ��Ų ����
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
