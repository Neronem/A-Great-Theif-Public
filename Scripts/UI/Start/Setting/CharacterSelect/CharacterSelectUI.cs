using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private string skinId;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
    private void Start()
    {
        bool unlocked = SkinManager.Instance.CheckSkinUnlocked(skinId);
        gameObject.SetActive(unlocked);
        _button.interactable = unlocked;

        SkinManager.Instance.OnSkinUnlocked += SkinUnlocked;
    }
    private void SkinUnlocked(string unlockedSkinId)
    {
        if (unlockedSkinId == skinId)
        {
            gameObject.SetActive(true);
            _button.interactable = true;
        }
    }

    private void OnClick()
    {
        SkinManager.Instance.SelectSkin(skinId);
        Debug.Log($"Selected skin: {skinId}");
    }
    private void OnDestroy()
    {
        if (SkinManager.Instance != null)
            SkinManager.Instance.OnSkinUnlocked -= SkinUnlocked;

        _button.onClick.RemoveListener(OnClick);
    }
}