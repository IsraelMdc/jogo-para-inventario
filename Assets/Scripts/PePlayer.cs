using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("objetos") || col.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Grounded(true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Grounded(false);
    }
}
