using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateToArrow : MonoBehaviour
{
    [SerializeField] gameManager manager;

    public void setArrows(int num, Vector3 arrowLoc, int specialOperator = 0)
    {
        if (specialOperator == 0)
        {
            if (num > 0)
            {
                Debug.Log(num);
                for (int i = 0; i < num; i++)
                {
                    manager.createArrow(arrowLoc);
                }
            }
            else
            {
                for (int i = 0; i < Mathf.Abs(num); i++)
                {
                    if (manager.arrows.transform.childCount > 1)
                    {
                        manager.destroyArrow();
                    }
                    else
                    {
                        manager.gameOver.SetActive(true);
                        Destroy(manager.arrows);
                        manager.stop = true;
                    }
                }
            }
        }
        else if (specialOperator == 1)
        {
            int stat = manager.createdArrows * (num - 1);
            for (int i = 0; i < stat; i++)
            {
                manager.createArrow(arrowLoc);
            }
        }
        else if (specialOperator == 2)
        {
            if (manager.createdArrows == 1 || manager.createdArrows / num <= 0)
            {
                manager.gameOver.SetActive(true);
                Destroy(manager.arrows);
                manager.stop = true;
            }
            else
            {
                int stat = manager.createdArrows / num;
                int stat2 = manager.createdArrows;
                for (int i = stat; i < stat2; i++)
                {
                    if (manager.arrows.transform.childCount > 1)
                    {
                        manager.destroyArrow();
                    }
                    else
                    {
                        manager.gameOver.SetActive(true);
                        Destroy(manager.arrows);
                        manager.stop = true;
                    }
                }
            }
        }
    }
}
