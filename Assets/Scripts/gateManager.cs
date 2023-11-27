using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class gateManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //populate the texts in their respective scripts with the static data retrieved from the menu manager, or open a new data manager present in both scenes with static info
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ssCen");
        if (collision.gameObject.name == "ArrowP")
        {
            Debug.Log("ssCen");
        }
    }
}
