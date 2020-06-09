using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public int jumpHeight = 300;
    private float moveX;
    private float moveY;
    public bool isGrounded;
    public LayerMask groundLayers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        
        isGrounded = Physics2D.OverlapArea(new 
            Vector2(transform.position.x - .5f, 
            transform.position.y - 1f), 
            new Vector2(transform.position.x + .5f, 
            transform.position.y - 1.1f), groundLayers);
        //isGrounded = Physics2D.OverlapCircle(transform.position, GetComponent<CircleCollider2D>().radius +.25f, groundLayers);

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //Application.Quit();
        }
        if (Input.GetKey(KeyCode.R) || Input.GetKey("joystick button 6"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Math.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) <= speed)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        else if (moveX > 0 && gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x + moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        else if (moveX < 0 && gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x + moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "water")
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 2;
            speed /= 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Mushroom")
        {
            //var x = gameObject.GetComponent<Rigidbody2D>();
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, 10);
        }
    }
    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "water")
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 0;
            speed *= 2;
        }
    }
}
