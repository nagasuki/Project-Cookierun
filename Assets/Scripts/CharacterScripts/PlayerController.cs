using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Character Status")]
    [SerializeField] private CharacterHealthViewModel characterHealthModel;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isSliding = false;
    private bool isGrounded = false;
    private float slideTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isRuning", true);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
            {
                StartSlide();
            }
        }

        if (isSliding)
        {
            slideTime -= Time.deltaTime;
            if (slideTime <= 0)
            {
                StopSlide();
            }
        }

        if (characterHealthModel.CurrentHealth <= 0)
        {
            animator.SetBool("isRuning", false);
            animator.SetBool("isDead", true);
            GameManager.Instance.GameOver().Forget();
        }

        animator.SetBool("isSliding", isSliding);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTime = slideDuration;
        animator.SetTrigger("Slide");

        GetComponent<CapsuleCollider2D>().size = new Vector2(1f, 0.5f);
    }

    private void StopSlide()
    {
        isSliding = false;

        GetComponent<CapsuleCollider2D>().size = new Vector2(1f, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Player is on the ground");
            isJumping = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Collision with obstacle");
            characterHealthModel.TakeDamage(1);
        }
    }
}
