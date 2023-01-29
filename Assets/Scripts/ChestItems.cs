using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItems : MonoBehaviour
{
    public Objects[] itensChest;
    public int[] quantItensChest;

    public Objects[] possiveisItensChest;
    public int[] quantMaxPossivelChest;
    public int[] quantMinPossivelChest;
    public bool aleatorizarChest = false;
    bool geracaoAleatoria = false;

    void Update()
    {
        if (aleatorizarChest)
        {
            if (!geracaoAleatoria)
            {
                aleatorizarItensChest();
                geracaoAleatoria = true;
            }
        }
    }

    void aleatorizarItensChest()
    {
        for (int i = 0; i < possiveisItensChest.Length; i++)
        {
            int quantiItem = Random.Range(quantMinPossivelChest[i], quantMaxPossivelChest[i] + 1);
            itensChest[i] = possiveisItensChest[i];
            quantItensChest[i] = quantiItem;
        }
    } 
}
