using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController: MonoBehaviour
{
    public GameObject inventoryPanel;
    public Text itemText;
    bool invActive = false;

    void Start()
    {
        itemText.text = null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            invActive =! invActive;
            inventoryPanel.SetActive(invActive);
        }
        if (invActive) 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
