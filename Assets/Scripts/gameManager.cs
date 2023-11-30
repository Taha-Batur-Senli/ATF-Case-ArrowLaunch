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
    [SerializeField] public TextMesh endCount;
    [SerializeField] public int width;
    [SerializeField] public List<string> powerUpList = new List<string>();
    public bool stop = false;
    public bool stopCamera = false;
    public int createdArrows = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameWon.SetActive(false);
        gameOver.SetActive(false);
        createArrow(Vector3.zero);
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
    public GameObject createArrow(Vector3 loc)
    {
        GameObject arrowToCreate = Instantiate(arrowPrefab);
        arrowToCreate.transform.SetParent(arrows.transform);
        arrowToCreate.transform.position = loc;
        createdArrows++;
        return arrowToCreate;
    }

    public void destroyArrow()
    {
        if(createdArrows > 1)
        {
            Destroy(arrows.transform.GetChild(createdArrows - 1).gameObject);
            createdArrows--;
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
