using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Para determinar se o player pode se mexer ou n�o
    bool canMove = true;

    [Header("Movement")]
    // Velocidade de movimento para andar e correr
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    // Para pular
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // Escolha da tecla para o pulo e para correr
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;


    // Checar se o player est� ou n�o no ch�o para se mover
    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask objetos;
    bool grounded;

    // Transform da orienta��o
    public Transform orientation;

    // Inputs do teclado do horizontal e vertical
    float horizontalInput;
    float verticalInput;

    // Dire��o de movimento
    Vector3 moveDirection;

    // Rigidbody do player
    Rigidbody rb;

    // Estado de movimento do player
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        // Pegar o rigidbody do player e congelar sua rota��o
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        if (canMove) {
            MyInput();
            SpeedControl();
            StateHandler();

            // A partir do cheque de ch�o faz o rag do player
            if (grounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        if (canMove) 
        {
            MovePlayer();
        }
    }


    private void MyInput()
    {
        // Input do teclado
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Quando player deve pular
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            // Resetar o pulo usando o cooldown para delay, algo que serve para n�o pular direto se segurar a tecla
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void StateHandler()
    {
        // Modo: Correndo (Sprinting)
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Modo: Andando
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Modo: No ar
        else
        {
            state = MovementState.air;
        }
    }
    private void MovePlayer()
    {
        // Calcular dire��o do movimento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Adicionar for�a no player
        // No ch�o
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // No ar
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        // Pega a velocidade normal do rigidbody
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    
        // Limita a velocidade se neccess�rio
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    
    }

    private void Jump()
    {
        // Confirma��o que a velocidade do y est� no 0
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Fazer a for�a d� pulo (usar o Impulse por estar fazendo for�a uma �nica vez)
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    // Fun��o s� para resetar o pulo
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void IfCanMove(int move)
    {
        if(move == 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    public void Grounded(bool onGround)
    {
        grounded = onGround;
    }

}   
