using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontal;
    private bool isGrounded;

    private bool wallLeft;
    private bool wallRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

        UpdateAnimations();
        FlipCharacter();
    }

    void FixedUpdate()
    {
        float movement = horizontal * moveSpeed;

        // Se estiver pressionando contra a parede,
        // não continua empurrando o personagem nela.
        if (wallRight && horizontal > 0)
            movement = 0;

        if (wallLeft && horizontal < 0)
            movement = 0;

        rb.linearVelocity = new Vector2(
            movement,
            rb.linearVelocity.y
        );
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        wallLeft = false;
        wallRight = false;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Parede à direita
            if (contact.normal.x < -0.5f)
            {
                wallRight = true;
            }

            // Parede à esquerda
            if (contact.normal.x > 0.5f)
            {
                wallLeft = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        wallLeft = false;
        wallRight = false;
    }

    void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("YVelocity", rb.linearVelocity.y);
    }
    void FlipCharacter()
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}