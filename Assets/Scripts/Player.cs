using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    // Velocidade de movimento
    public float moveSpeed;

    public float groundDrag;

    // Para pular
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // Escolha da tecla para o pulo
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    // Checar se o player est� ou n�o no ch�o para se mover
    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
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

    private void Start()
    {
        // Pegar o rigidbody do player e congelar sua rota��o
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // Usa o raycast apontado para baixo para checar se o player est� ou n�o no ch�o
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

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

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        // Input do teclado
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Quando player deve pular
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Debug.Log("Entrou");
            readyToJump = false;

            Jump();

            // Resetar o pulo usando o cooldown para delay, algo que serve para n�o pular direto se segurar a tecla
            Invoke(nameof(ResetJump), jumpCooldown);
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
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
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

}   
