using UnityEngine;
using System.Collections;

public class loop : MonoBehaviour
{

    public float speed, JumpSpace,LeftLimit;
    GameObject cam;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }


    void Update()
    {
        if (transform.localPosition.x>LeftLimit)
        {

            transform.position = new Vector3(transform.position.x - speed / 80f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x+ JumpSpace, transform.position.y, transform.position.z);
        }
    }
}
