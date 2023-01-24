using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    // Se a c�mera pode ou n�o se mexer
    bool canMove = true;

    // Sensibilidade para o x e y  
    public float sensX;
    public float sensY;

    // Orienta��o do player
    public Transform orientation;

    // Rota��o x e y da c�mera
    float xRotation;
    float yRotation;

    private void Start()
    {
        // Trancar o cursor no meio da tela, e fazer dele invis�vel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canMove)
        {
            // Pegar o input do mouse
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            // Jeito que o unity funciona com rota��o
            yRotation += mouseX;
            xRotation -= mouseY;

            // Para o player n�o olhar mais que 90 graus para cima ou baixo
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rota��o da c�mera e orienta��o
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    public void IfCanMove(int move)
    {
        if (move == 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
}
