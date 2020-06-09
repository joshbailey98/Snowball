using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandRemover : MonoBehaviour {
    Transform Camera;
	void Start () {
        Camera = GameObject.Find("Main Camera").transform;
	}
	
	void Update () {
        if (transform.position.x<Camera.position.x- 20)
        {
            Destroy(gameObject);
        }
	}
}
