using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class gateManager : MonoBehaviour
{
    [SerializeField] rightScript right;
    [SerializeField] leftScript left;

    // Start is called before the first frame update
    void Start()
    {
        //populate the texts in their respective scripts with the static data retrieved from the menu manager, or open a new data manager present in both scenes with static info
    }

    public void alterArrows(int value, int specialoperator = 0)
    {
        gameObject.transform.parent.GetComponent<gateToArrow>().setArrows(value, specialoperator);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int dice = -1;

        if (collision.gameObject.GetComponent<CapsuleCollider>() != null)
        {
            dice = Random.Range(0,1);

            if(dice == 0)
            {
                //do left

            }
            else
            {
                //do right
            }
        }
    }
}
