using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rightScript : MonoBehaviour
{
    [SerializeField] public TextMesh text;
    [SerializeField] GameObject gate;
    string operation;
    int num;
    public bool doOnce = true;

    //populate the texts in their respective scripts with the static data retrieved from the menu manager, or open a new data manager present in both scenes with static info

    public void readData(Vector3 loc)
    {
        operation = text.text;
        num = int.Parse(operation.Substring(1));
        operation = operation[0].ToString().Trim();

        if (operation == "+")
        {
            gate.GetComponent<gateManager>().alterArrows(num, loc);
        }
        else if (operation == "-")
        {
            gate.GetComponent<gateManager>().alterArrows(-num, loc);
        }
        else if (operation == "*")
        {
            gate.GetComponent<gateManager>().alterArrows(num, loc, 1);
        }
        else if (operation == "/")
        {
            gate.GetComponent<gateManager>().alterArrows(num, loc, 2);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //add collision check here
        if (collision.gameObject.GetComponent<arrowManager>())
        {
            if (doOnce)
            {
                readData(collision.transform.parent.GetChild(0).transform.position);
            }
            doOnce = false;
            gate.GetComponent<gateManager>().clearAll();
        }
    }
}
