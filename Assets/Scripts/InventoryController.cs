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

    // Baú
    int abriuBauCont = 0;
    bool abriuBau = false;
    public Sprite slotVazio;

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
        // Função que checa se um baú foi aberto
        AbriuBau();

        // Raycast do aim do player
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
                if (abriuBauCont < 2)
                {
                    abriuBauCont++;
                }
                if (abriuBau == false && abriuBauCont == 2)
                {
                    iController.itemText.text = "Press (E) to open the Chest";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        slotsChest = hit.transform.GetComponent<ChestItems>().itensChest;
                        slotAmountChest = hit.transform.GetComponent<ChestItems>().quantItensChest;
                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<InterfaceController>().Chest(0);
                        abriuBau = true;
                    }
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

    void AbriuBau()
    {
        if(abriuBau == true)
        {/**
            for (int i = 0; i < slotsInv.Length; i++)
            {

            }**/

            if (abriuBauCont < 4)
            {
                SyncInventories(0);
                abriuBauCont++;
            }
            // Desativar o menu de baú
            if(Input.GetKeyDown(KeyCode.E) && abriuBauCont == 4)
            {
                abriuBauCont = 0;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<InterfaceController>().Chest(1);
                SyncInventories(1);
                abriuBau = false;
            }
        }
    }


    // Função que vai atualizar o inventário que aparece ao abrir baús com o inventário normal. Ao receber os seguintes números:
    // 0: Atualiza o inventário para báu
    // 1: Atualiza o inventário normal
    void SyncInventories(int invParaAtualizar)
    {
        if(invParaAtualizar == 0)
        {
            for (int i = 0; i < slotsInv.Length; i++)
            {
                if (slotsInv[i] != null)
                {
                    slotsChestInv[i] = slotsInv[i];
                    slotAmountChestInv[i] = slotAmountInv[i];
                    quantidadeFundoImageChestInv[i].gameObject.SetActive(true);
                    quantidadeImageChestInv[i].gameObject.SetActive(true);
                    quantidadesTextChestInv[i].GetComponent<Text>().text = slotAmountChestInv[i].ToString();
                    slotImageChestInv[i].sprite = slotsInv[i].itemSprite;
                }
            }

            for (int i = 0; i < slotsChest.Length; i++)
            {
                if (slotsChest[i] != null && slotAmountChest[i] != 0)
                {
                    quantidadeFundoImageChest[i].gameObject.SetActive(true);
                    quantidadeImageChest[i].gameObject.SetActive(true);
                    quantidadesTextChest[i].GetComponent<Text>().text = slotAmountChest[i].ToString();
                    slotImageChest[i].sprite = slotsChest[i].itemSprite;
                }
                else
                {
                    quantidadeFundoImageChest[i].gameObject.SetActive(false);
                    quantidadeImageChest[i].gameObject.SetActive(false);
                    slotImageChest[i].sprite = slotVazio;
                }
            }

        }
        else
        {
            for (int i = 0; i < slotsChestInv.Length; i++)
            {
                if (slotsChestInv[i] != null)
                {
                    slotsInv[i] = slotsChestInv[i];
                    slotAmountInv[i] = slotAmountChestInv[i];
                    quantidadeFundoImageInv[i].gameObject.SetActive(true);
                    quantidadeImageInv[i].gameObject.SetActive(true);
                    quantidadesTextInv[i].GetComponent<Text>().text = slotAmountInv[i].ToString();
                    slotImageInv[i].sprite = slotsChestInv[i].itemSprite;
                }
                else
                {
                    quantidadeFundoImageInv[i].gameObject.SetActive(false);
                    quantidadeImageInv[i].gameObject.SetActive(false);
                    slotImageInv[i].sprite = slotVazio;
                }
            }
        }
    }

    // Função que ao ser chamada permite a transferência de itens.
    // Caso lado seja 0, o slot está do aldo do inventário do baú
    // Caso seja 1, está no próprio baú
    public void TransferirItem(char slotNum, int lado)
    {
        if(lado == 0)
        {
            // A cada scroll para cima
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                int slot = slotNum;
                if (slotsChestInv[slot - 1] != null)
                {

                }
                
               
            }
            // A cada scroll para baixo
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
            }
        }
        else
        {
        }
    }

}
