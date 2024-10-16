using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpForceInPlane = 10f;
    [SerializeField] private float hitDuration = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private EndlessMapGenerator endlessMapGenerator;

    [Header("Character Status")]
    [SerializeField] private CharacterHealthViewModel characterHealthModel;
    [SerializeField] private ScoreViewModel scoreViewModel;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isSliding = false;
    private bool isGrounded = false;
    private bool isHit = false;
    private bool isDash = false;
    private float hitTime = 0f;

    public bool IsDash => isDash;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetTrigger("Runing");
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGameComplete) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !isJumping && !isDash)
        {
            if (Input.GetKey(KeyCode.UpArrow) && !isSliding)
            {
                Jump();
                Debug.Log("Jump");
            }

            if (Input.GetKey(KeyCode.DownArrow) && !isSliding)
            {
                StartSlide();
                Debug.Log("Start Slide");
            }

            if (Input.GetKeyUp(KeyCode.DownArrow) && isSliding)
            {
                StopSlide();
                Debug.Log("Stop Slide");
            }
        }

        if (characterHealthModel.CurrentHealth <= 0)
        {
            animator.SetBool("isRuning", false);
            animator.SetBool("isDead", true);
            GameManager.Instance.GameOver(scoreViewModel.CurrentScore).Forget();
        }

        if (isHit)
        {
            hitTime -= Time.deltaTime;
            if (hitTime <= 0)
            {
                animator.SetLayerWeight(1, 0f);
                isHit = false;
            }
        }

        if (isDash)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForceInPlane);
            }
        }

        if (transform.position.y < -10)
        {
            GameManager.Instance.GameOver(scoreViewModel.CurrentScore).Forget();
        }

        animator.SetBool("isSliding", isSliding);
        animator.SetBool("isDash", isDash);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        SoundManager.Instance.PlayeSFX("Jump");
    }

    private void StartSlide()
    {
        isSliding = true;
        animator.SetTrigger("Slide");

        GetComponent<CapsuleCollider2D>().offset = new Vector2(.1f, .4f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(1f, 1f);
    }

    private void StopSlide()
    {
        isSliding = false;

        GetComponent<CapsuleCollider2D>().offset = new Vector2(.1f, .9f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(1f, 2f);
    }

    public void StartDashBoost()
    {
        isDash = true;

        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(-6.6f, 0, 0);

        animator.SetTrigger("Dashing");
        endlessMapGenerator.StartDashBoost();
    }

    public void StopDashBoost()
    {
        isDash = false;

        rb.gravityScale = 2f;

        animator.SetTrigger("Runing");
        endlessMapGenerator.StopDashBoost();
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
            if (!isHit && !IsDash)
            {
                Debug.Log("Collision with obstacle");
                characterHealthModel.TakeDamage(1);
                isHit = true;
                hitTime = hitDuration;
                animator.SetLayerWeight(1, 1f);
            }
        }
    }
}
