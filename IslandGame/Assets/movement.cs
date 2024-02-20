using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    float normalSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if(inputX != 0 && inputY != 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, inputY * speed);
    }
}
