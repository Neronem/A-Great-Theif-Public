using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera: MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    private Transform target;
    float offsetX;

    void Start()
    {
        GameObject go = GameObject.FindWithTag(targetTag);
        if (go != null)
        {
            target = go.transform;
            offsetX = transform.position.x - target.position.x;
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}