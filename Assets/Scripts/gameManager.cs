using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] public GameObject arrows;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public int width;
    [SerializeField] public List<string> powerUpList = new List<string>();
    bool stop = false;
    int createdArrows = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        createArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
        }
    }

    public GameObject createArrow()
    {
        GameObject arrowToCreate = Instantiate(arrowPrefab);
        arrowToCreate.transform.SetParent(arrows.transform);
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
