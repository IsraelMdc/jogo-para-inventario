using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour
{
    public Objects[] slots;
    public Image[] slotImage;
    public int[] slotAmount;
    private float rangeRay = 5f;

    private InterfaceController iController;

    void Start()
    {
        iController = FindObjectOfType<InterfaceController>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        if (Physics.Raycast(ray, out hit, rangeRay)) 
        {
            if (hit.collider.tag == "Object")
            {
                iController.itemText.text = "Press (E) to collect the " + hit.transform.GetComponent<ObjectType>().objectype.name;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {

                        // Adiciona caso ja exista ou caso for o Item type igual adiciona mais um.
                        if (slots[i] == null || slots[i].name == hit.transform.GetComponent<ObjectType>().objectype.name)
                        {
                            //adiciona ao slot o objeto em observação
                            slots[i] = hit.transform.GetComponent<ObjectType>().objectype;
                            //Incrementa a quantidade do item caso ja tenha ou gera caso n tenha
                            slotAmount[i]++;
                            //adiciona imagem a ele
                            slotImage[i].sprite = slots[i].itemSprite;

                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            } 
            
            else if(hit.collider.tag != "Object") 
            {
                iController.itemText.text = null;
            }
        }
    }
}
