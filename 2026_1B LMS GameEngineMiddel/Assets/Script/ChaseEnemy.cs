using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // 실수로 누락하지 않도록 자동 추가
public class ChaseEnemy : MonoBehaviour
{
    public Transform player;      // 플레이어의 위치
    public float moveSpeed = 3f;  // 이동 속도
    public float chaseRange = 10f; // 추적을 시작할 인식 범위

    private Rigidbody2D rb;
    private bool isChasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 만약 인스펙터에서 플레이어를 할당하지 않았다면 "Player" 태그로 찾음
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        // 플랫포머에서 적이 회전하면서 넘어지지 않게 고정
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null) return;

        // 플레이어와 적 사이의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }

    void FixedUpdate()
    {
        if (isChasing && player != null)
        {
            // 플레이어 방향 계산 (왼쪽/오른쪽)
            float direction = (player.position.x > transform.position.x) ? 1 : -1;

            // 물리적으로 이동 (X축 속도만 변경)
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

            // 적 캐릭터가 이동 방향을 바라보게 회전 (선택 사항)
            transform.localScale = new Vector3(direction, 1, 1);
        }
        else
        {
            // 추적 범위 밖이면 멈춤 (X축만)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    // 인식 범위를 에디터에서 시각적으로 확인하기 위함
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}