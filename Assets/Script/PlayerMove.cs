using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Salto")]
    public float jumpForce = 10f;

    [Header("Detecci�n de Suelo")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private PlayerInput inputActions;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInput();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        CheckGround();

        // PC: salto con teclado (Space por defecto en Input System)
        if (inputActions.Player.Jump.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
    }

    // Detecta cuando tocamos algo
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si tocamos un obst�culo
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("�AUCH! Perdiste una vida");            
            // Por ahora solo mostrar mensaje
            GameManager.Instance.LoseLife();
        }
    }


    // GIZMO 
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

}

