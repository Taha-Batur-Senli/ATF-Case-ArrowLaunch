using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowManager : MonoBehaviour
{
    [SerializeField] public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            transform.Translate(new Vector3(0, 0, 25) * Time.deltaTime);
        }
    }
}
