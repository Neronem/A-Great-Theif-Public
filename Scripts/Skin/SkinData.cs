using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    public string skinId;

    public string skinName;

    public GameObject skinPrefab;

    public bool defaultUnlocked = false;

    public bool isUnlocked = false;

    public float maxHealth = 100f;

    public float jumpForce = 20f;

    public AnimatorOverrideController animatorOverride;

}