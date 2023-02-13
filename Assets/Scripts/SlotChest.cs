using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotChest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public char slotNum;
    public int lado;
    bool mouseOver = false;

    void Update()
    {
        if (mouseOver)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>().TransferirItem(slotNum, lado);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotNum = eventData.pointerEnter.transform.name[5];
        if(eventData.pointerEnter.transform.parent.parent.name == "Chest Inv")
        {
            lado = 0;
        }
        else
        {
            lado = 1;
        }

        mouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
