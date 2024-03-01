using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerMulti : MonoBehaviour
{
    //Variables de referencia
    private Animator anim;
    private Rigidbody2D playerRb;
    private PlayerInput playerInput;
    private Vector2 horInput;
    public enum PlayerState { normal, damaged }

    [Header("Character Stats")]
    public float speed;
    public float jumpForce;

    [Header("GroundCheck Configuration")]
    [SerializeField] bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [Header("Character Status")]
    private bool isFacingRight = true;
    private bool canAttack = true;
    [SerializeField] PlayerState currentState;

    [Header("Knockback Settings")]
    public float knockbackMultiplier; //Multiplicador de la fuerza de empuje. Puede modificarse a través de power-ups al ser público
    [SerializeField] float knockbackX = 70; //Fuerza de empuje en el eje X
    [SerializeField] float knockbackY; //Fuerza de empuje en el eje Y
    Vector2 knockbackForce; //Dirección hacia la que se impulsa el pj al recibir una patada

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        currentState = PlayerState.normal;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("Jump", !isGrounded);
        FlipUpdater();

        if (currentState == PlayerState.normal)
        {
            horInput = playerInput.actions["Move"].ReadValue<Vector2>();
        }

    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.normal) { Movement(); }
            
    }

    void Movement()
    {
        playerRb.velocity = new Vector2(horInput.x * speed, playerRb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            if (currentState == PlayerState.normal)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
            
        }
    }

    void FlipUpdater()
    {
        if (horInput.x > 0)
        {
            anim.SetBool("Run", true);
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if (horInput.x < 0)
        {
            anim.SetBool("Run", true);
            if (isFacingRight)
            {
                Flip();
            }
        }
        if (horInput.x == 0)
        {
            anim.SetBool("Run", false);
        }
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && currentState == PlayerState.normal)
        {
            if (canAttack)
            {
                anim.SetTrigger("Kick");
                canAttack = false;
                Invoke("ResetAttack", 3f);
            }
            
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void ResetState()
    {
        anim.SetBool("isDamaged", false);
        currentState = PlayerState.normal;
    }


    //TOCAR PROGRAMACION DE ESTO
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && currentState == PlayerState.normal)
        {
            anim.SetTrigger("Hit");
            anim.SetBool("isDamaged", true);
            currentState = PlayerState.damaged;
            //Si el que pega la patada está a la izquierda
            if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                knockbackForce = new Vector2(knockbackX, knockbackY); //Determinamos la dirección de empuje hacia la derecha
                playerRb.AddForce(knockbackForce * knockbackMultiplier); //Aplicamos fuerza de empuje en la dirección deseada por el multiplicador de empuje.
            }
            else //Si el que pega la patada está a la derecha
            {
                knockbackForce = new Vector2(-knockbackX, knockbackY); //Determinamos la dirección de empuje hacia la izquierda
                playerRb.AddForce(knockbackForce * knockbackMultiplier); //Aplicamos fuerza de empuje en la dirección deseada por el multiplicador de empuje.
            }
            Invoke("ResetState", 1.5f);
        }
    }
}
