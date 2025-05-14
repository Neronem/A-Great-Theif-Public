using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField] int isFirst;

    private void Start()
    {
        isFirst = PlayerPrefs.GetInt("isFirst", 1);

        if (isFirst != 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("isFirst", 0);
            PlayerPrefs.Save();
            this.gameObject.SetActive(false);
        }
    }
}

