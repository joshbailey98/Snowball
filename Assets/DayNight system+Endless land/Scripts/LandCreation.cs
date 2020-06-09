using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCreation : MonoBehaviour
{
    int order;
    GameObject cam;
    float y;
    void Start()
    {

        order = 0;
        cam = GameObject.Find("Main Camera");
        y = transform.position.y;
    }

    void Update()
    {
        if (cam.transform.position.x > transform.position.x-15)
        {
           order++;
            gameObject.transform.position = new Vector3(order * 7.68f,y, 0);
            Instantiate(Resources.Load("land" + Random.Range(1, 5)), transform.position, transform.rotation);
        }
    }
}
