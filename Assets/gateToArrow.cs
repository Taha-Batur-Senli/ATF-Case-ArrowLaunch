using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateToArrow : MonoBehaviour
{
    [SerializeField] gameManager manager;

    public void setArrows(int num, int specialOperator = 0)
    {
        if (specialOperator == 0)
        {
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    manager.createArrow();
                }
            }
            else
            {
                for (int i = 0; i < num; i++)
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
            for (int i = 0; i < manager.createdArrows * (num - 1); i++)
            {
                manager.createArrow();
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
                for (int i = manager.createdArrows / num; i < manager.createdArrows; i++)
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
