using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public static AchievementUI Instance;
    [SerializeField] private TextMeshProUGUI achievementText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowAchievementText(string text)
    {
        gameObject.SetActive(true);
        achievementText.text = text;
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }
    private IEnumerator ShowRoutine()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
