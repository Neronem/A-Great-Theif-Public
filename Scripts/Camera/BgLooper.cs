using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgLooper : MonoBehaviour
{
    ObstacleManager obstacleManager;

    int numOfBg = 5; //백그라운드 개수
    Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        obstacleManager = ObstacleManager.Instance;
        obstacleLastPosition = new Vector3(-5, -3);
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            float widthOfObstacle = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfObstacle;
            ObstacleManager.Instance.CreateObstacle(pos);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numOfBg;
            collision.transform.position = pos;
            return;
        }

        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }


}