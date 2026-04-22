using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("점프 설정")]
    public int maxJumpCount = 1;
    private int currentJumpCount = 0;

    [Header("바닥 체크")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.15f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 바닥 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 착지 시 점프 초기화
        if (isGrounded && rb.linearVelocity.y <= 0.01f)
        {
            currentJumpCount = 0;
        }

        // 낙사 처리
        if (transform.position.y < -10f) RestartScene();
    }

    private void FixedUpdate()
    {
        // 물리 이동은 FixedUpdate에서 하는 것이 물리 엔진과 동기화되어 더 부드럽습니다.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && currentJumpCount < maxJumpCount)
        {
            // 점프 전 속도를 0으로 만들어 일정한 점프 높이 유지
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            currentJumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 추적형 적이나 고정형 적 모두 "Enemy" 태그를 써주세요.
        if (collision.gameObject.CompareTag("Enemy")) RestartScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike")) RestartScene();

        // 아이템 태그를 "JumpItem"으로 설정하고 Is Trigger 체크 필수!
        if (collision.CompareTag("JumpItem"))
        {
            maxJumpCount = 2;
            Destroy(collision.gameObject);
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}