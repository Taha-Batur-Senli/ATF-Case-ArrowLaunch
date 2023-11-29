using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] public GameObject arrows;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject left;
    [SerializeField] public GameObject right;
    [SerializeField] public int width;
    [SerializeField] public List<string> powerUpList = new List<string>();
    public bool stop = false;
    public int createdArrows = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        createArrow(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
            arrows.transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);

            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Screen.width) && !hit.collider.gameObject.Equals(left) && !hit.collider.gameObject.Equals(right))
            {
                arrows.transform.position = new Vector3(hit.point.x, arrows.transform.position.y, arrows.transform.position.z);
            }
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
        if(arrows.transform.childCount > 1)
        {
            Destroy(arrows.transform.GetChild(0).gameObject);
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

    
}
