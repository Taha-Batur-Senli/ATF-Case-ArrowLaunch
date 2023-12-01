using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class gateManager : MonoBehaviour
{
    [SerializeField] rightScript right;
    [SerializeField] leftScript left;
    bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        //populate the texts in their respective scripts with the static data retrieved from the menu manager, or open a new data manager present in both scenes with static info
    }

    public void populate(string leftT, string rightT)
    {
        left.text.text = leftT;
        right.text.text = rightT;
    }

    public void alterArrows(int value, Vector3 loc, int specialoperator = 0)
    {
        gameObject.transform.parent.GetComponent<gateToArrow>().setArrows(value, loc, specialoperator);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(doOnce)
        {
            int dice = -1;

            if (collision.gameObject.GetComponent<CapsuleCollider>() != null)
            {
                dice = Random.Range(0, 1);

                if (dice == 0)
                {
                    //do left
                    left.readData(collision.transform.position);

                }
                else
                {
                    //do right
                    right.readData(collision.transform.position);
                }
            }
        }
        doOnce = false;
    }

    public void clearAll()
    {
        doOnce = false;
        left.doOnce = false;
        right.doOnce = false;
    }
}
