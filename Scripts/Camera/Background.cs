using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background : MonoBehaviour
{
    Tilemap bg;
    Color morningColor = new Color(1.0f, 0.7f, 0.5f); // ���� ��Ȳ
    Color noonColor = new Color(1.0f, 1.0f, 1.0f);     // �Ͼ��
    Color eveningColor = new Color(0.5f, 0.3f, 0.7f);  // ������
    Color nightColor = new Color(0.1f, 0.1f, 0.2f);    // ��ο� ��

    float timeCycleDuration = 60f;

    // Start is called before the first frame update
    void Start()
    {
        bg = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time % timeCycleDuration;
        float t = time / timeCycleDuration;

        Color currentColor;

        if (t < 0.25f) // ��ħ �� ��
        {
            float localT = t / 0.25f;
            currentColor = Color.Lerp(morningColor, noonColor, localT);
        }
        else if (t < 0.5f) // �� �� ����
        {
            float localT = (t - 0.25f) / 0.25f;
            currentColor = Color.Lerp(noonColor, eveningColor, localT);
        }
        else if (t < 0.75f) // ���� �� ��
        {
            float localT = (t - 0.5f) / 0.25f;
            currentColor = Color.Lerp(eveningColor, nightColor, localT);
        }
        else // �� �� ��ħ
        {
            float localT = (t - 0.75f) / 0.25f;
            currentColor = Color.Lerp(nightColor, morningColor, localT);
        }

        bg.color = currentColor;
    }
}
