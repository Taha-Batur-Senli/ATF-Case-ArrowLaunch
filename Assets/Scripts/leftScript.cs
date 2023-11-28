using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leftScript : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameObject gate;
    string operation;
    int num;

    //populate the texts in their respective scripts with the static data retrieved from the menu manager, or open a new data manager present in both scenes with static info
    public void populate()
    {

    }

    public void readData()
    {
        operation = text.ToString()[0].ToString().Trim();

        num = int.Parse(text.ToString().Substring(1));

        if (operation == "+")
        {
            gate.GetComponent<gateManager>().alterArrows(num);
        }
        else if (operation == "-")
        {
            gate.GetComponent<gateManager>().alterArrows(-num);
        }
        else if (operation == "x")
        {
            gate.GetComponent<gateManager>().alterArrows(num, 1);
        }
        else if (operation == "/")
        {
            gate.GetComponent<gateManager>().alterArrows(num, 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //add collision check here
        if(collision.gameObject.name == "ArrowP")
        {
            Debug.Log("ss");
        }
    }
}
