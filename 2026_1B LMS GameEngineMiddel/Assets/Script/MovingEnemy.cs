using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float moveRange = 2f;   // 이동 범위
    public float speed = 2f;       // 이동 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, moveRange);
        transform.position = new Vector3(
            startPos.x,
            startPos.y + y,
            startPos.z
        );
    }
}