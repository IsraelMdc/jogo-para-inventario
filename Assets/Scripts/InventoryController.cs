using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour
{
    // Inventário do player
    public Objects[] slotsInv;
    public Image[] slotImageInv;
    public Image[] quantidadeImageInv;
    public Image[] quantidadeFundoImageInv;
    public Text[] quantidadesTextInv;
    public int[] slotAmountInv;

    // Espaço de um baú
    public Objects[] slotsChest;
    public Image[] slotImageChest;
    public Image[] quantidadeImageChest;
    public Image[] quantidadeFundoImageChest;
    public Text[] quantidadesTextChest;
    public int[] slotAmountChest;

    // Inventário do player quando um baú é aberto
    public Objects[] slotsChestInv;
    public Image[] slotImageChestInv;
    public Image[] quantidadeImageChestInv;
    public Image[] quantidadeFundoImageChestInv;
    public Text[] quantidadesTextChestInv;
    public int[] slotAmountChestInv;

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
                    for (int i = 0; i < slotsInv.Length; i++)
                    {

                        // Adiciona caso ja exista ou caso for o Item type igual adiciona mais um.
                        if (slotsInv[i] == null || slotsInv[i].name == hit.transform.GetComponent<ObjectType>().objectype.name)
                        {
                            // Adiciona ao slot o objeto em observação
                            slotsInv[i] = hit.transform.GetComponent<ObjectType>().objectype;
                            // Incrementa a quantidade do item caso ja tenha ou gera caso n tenha
                            slotAmountInv[i]++;
                            // Ativa a imagem da quantidade do item e seu fundo
                            quantidadeFundoImageInv[i].gameObject.SetActive(true);
                            quantidadeImageInv[i].gameObject.SetActive(true);
                            // Altera a quantidade descrita do item
                            quantidadesTextInv[i].GetComponent<Text>().text = slotAmountInv[i].ToString();
                            // Adiciona imagem a ele
                            slotImageInv[i].sprite = slotsInv[i].itemSprite;
                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            } 
            else if(hit.collider.tag == "Chest")
            {
                iController.itemText.text = "Press (E) to open the Chest";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject.FindGameObjectWithTag("Canvas").GetComponent<InterfaceController>().Chest(0);
                    /**
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<InterfaceController>().Chest(1);
                    }**/

                }
            }
            else if(hit.collider.tag != "Object") 
            {
                iController.itemText.text = null;
            }
        }
        else
        {
            iController.itemText.text = null;
        }
    }

    // Função que vai atualizar o inventário que aparece ao abrir baús com o inventário normal. Ao receber os seguintes números:
    // 0: Atualiza o inventário para báu
    // 1: Atualiza o inventário normal
    void SyncInventories(int invParaAtualizar)
    {
        // A criar
    }
}
