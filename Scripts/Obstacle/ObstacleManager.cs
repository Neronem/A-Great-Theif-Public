using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;
    GameManager gameManager;
    Collider2D collider;

    List<GameObject> obstaclePrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        collider = GetComponent<Collider2D>();

        LoadObstacles();
        Vector3 firstPos = new Vector3(-5, -4, -1);
        Instantiate(obstaclePrefabs[0], firstPos, Quaternion.identity);
    }

    //��ֹ� ������ �ҷ��� ����Ʈ�� ����
    void LoadObstacles()
    {
        for (int i = 0; i < 8; i++)
        {
            string prefabName = "obstacles_" + i;
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
            if (prefab != null)
            {
                obstaclePrefabs.Add(prefab);
            }
        }
    }

    //��ֹ� ���� ����
    public void CreateObstacle(Vector3 pos)
    {
        GameObject random = obstaclePrefabs[Random.Range(1, obstaclePrefabs.Count)];
        Instantiate(random, pos, Quaternion.identity);
    }
}