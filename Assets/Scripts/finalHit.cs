using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalHit : MonoBehaviour
{
    [SerializeField] gameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponent<arrowManager>())
        {
            gameManager.endGame();
        }
    }
}
