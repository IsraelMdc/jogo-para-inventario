using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    // Se a câmera pode ou não se mexer
    bool canMove = true;

    // Sensibilidade para o x e y  
    public float sensX;
    public float sensY;

    // Orientação do player
    public Transform orientation;

    // Rotação x e y da câmera
    float xRotation;
    float yRotation;

    private void Start()
    {
        // Trancar o cursor no meio da tela, e fazer dele invisível
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

            // Jeito que o unity funciona com rotação
            yRotation += mouseX;
            xRotation -= mouseY;

            // Para o player não olhar mais que 90 graus para cima ou baixo
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rotação da câmera e orientação
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
