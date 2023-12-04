using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] public GameObject arrows;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject gameWon;
    [SerializeField] public GameObject backgroundL;
    [SerializeField] public GameObject backgroundR;
    [SerializeField] public GameObject secondBackground;
    [SerializeField] public GameObject ground;
    [SerializeField] public GameObject finalBlock;
    [SerializeField] public GameObject gate;
    [SerializeField] public GameObject gateList;
    [SerializeField] public TextMesh endCount;
    [SerializeField] public int width;
    [SerializeField] public menuManager menuData;
    [SerializeField] public List<string> powerUpList = new List<string>();
    [SerializeField] public int howManyOnOneRow = 5;
    public bool stop = false;
    public bool stopCamera = false;
    public int createdArrows = 0;
    int arrowOnDown = 0;
    int arrowLeft = 0;
    int arrowRight = 0;
    int arrowRow = 0;
    GameObject firstArrow;

    // Start is called before the first frame update
    void Start()
    {
        gameWon.SetActive(false);
        gameOver.SetActive(false);
        createArrow(Vector3.zero, true);

        endCount.text = menuData.getLevel().ToString();

        for(int i = 0; i < menuData.getLeft().Length; i++)
        {
            GameObject gateToCreate = Instantiate(gate);
            gateToCreate.transform.SetParent(gateList.transform);
            gateToCreate.transform.position = new Vector3(0,0, 70 * (i+1));
            gateToCreate.GetComponent<gateManager>().populate(menuData.getLeft()[i], menuData.getRight()[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if(!stopCamera)
            {
                transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
            }

            arrows.transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);

            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Screen.width) && !hit.collider.gameObject.Equals(backgroundL) && !hit.collider.gameObject.Equals(backgroundR) &&!hit.collider.gameObject.Equals(secondBackground))
            {
                arrows.transform.position = new Vector3(hit.point.x, arrows.transform.position.y, arrows.transform.position.z);
            }
        }
    }

    public void endGame()
    {
        if(arrows.transform.childCount >= int.Parse(endCount.text.ToString()))
        {
            //stop and create final game over canvas also destroy the door
            gameWon.SetActive(true);
            Destroy(arrows);
            stop = true;
            Destroy(finalBlock);

        }
        else
        {
            gameOver.SetActive(true);
            Destroy(arrows);
            stop = true;
        }
    }

    public GameObject createArrow(Vector3 loc, bool first = false)
    {
        GameObject arrowToCreate = Instantiate(arrowPrefab);
        arrowToCreate.transform.SetParent(arrows.transform);
        Debug.Log(arrowRight);

        if(arrowLeft < 0)
        {
            arrowLeft = 0;
        }
        
        if(arrowRight < 0)
        {
            arrowRight = 0;
        }

        if (first)
        {
            firstArrow = arrowToCreate;
        }
        else
        {
            if (arrowOnDown % howManyOnOneRow == 0)
            {
                arrowRight = 0;
                arrowLeft = 0;
            }
        }

        arrowToCreate.transform.position = firstArrow.gameObject.transform.position;

        if (arrowLeft == 0 && arrowRight == 0)
        {
            arrowToCreate.transform.position += new Vector3(0, 2.2f * arrowRow, 0);
            arrowLeft++;
            arrowRight++;
        }
        else if (arrowOnDown % 2 == 0)
        {
            arrowToCreate.transform.position += new Vector3(2.2f * arrowRight , 2.2f * arrowRow, 0);
            arrowRight++;
        }
        else
        {
            arrowToCreate.transform.position += new Vector3(-2.2f * arrowLeft, 2.2f * arrowRow, 0);
            arrowLeft++;
        }

        arrowOnDown++;

        if(arrowOnDown % howManyOnOneRow == 0)
        {
            arrowRow++;
            arrowOnDown = 0;
            arrowRight = 0;
            arrowLeft = 0;
        }

        createdArrows++;
        return arrowToCreate;
    }

    public void destroyArrow()
    {
        if(createdArrows > 1)
        {
            createdArrows--;

            if (arrowLeft < 0)
            {
                arrowLeft = 0;
            }

            if (arrowRight < 0)
            {
                arrowRight = 0;
            }

            if (arrowLeft == 0 && arrowRight == 0)
            {
                arrowRow--;
                arrowOnDown = howManyOnOneRow - 1;
                arrowLeft = (howManyOnOneRow / 2) + 1;
                arrowRight = (howManyOnOneRow / 2) + 1;
            }
            else
            {
                arrowOnDown--;
            }

            if (arrows.transform.GetChild(createdArrows).gameObject.transform.localPosition.x == 0)
            {
                arrowRight--;
                arrowLeft--;
            }
            else if(arrows.transform.GetChild(createdArrows).gameObject.transform.localPosition.x > 0)
            {
                arrowRight--;
            }
            else if(arrows.transform.GetChild(createdArrows).gameObject.transform.localPosition.x < 0)
            {
                arrowLeft--;
            }

            Destroy(arrows.transform.GetChild(createdArrows).gameObject);

            if (arrowOnDown == 0)
            {
                arrowRight = 0;
                arrowLeft = 0;
            }
        }
        else
        {
            gameOver.SetActive(true);
            Destroy(arrows);
            stop = true;
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void nextLevel()
    {
        //SceneManager.LoadScene("GameplayScene");
    }

    public void goBack()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
