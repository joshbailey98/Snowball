using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var bounceDirection = collision.transform.position - gameObject.transform.position;
            if (bounceDirection.y >= 0)
                collision.rigidbody.AddForce(bounceDirection * 400);
        }
    }
}
