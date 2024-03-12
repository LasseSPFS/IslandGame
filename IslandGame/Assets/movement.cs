using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    float normalSpeed;
    Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
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
        if(rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        }
        if (rb.velocity.x == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, inputY * speed);
        }
        whichWayAmILooking();
        //rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        //rb.velocity = new Vector2(rb.velocity.x, inputY * speed);
    }



    public void whichWayAmILooking()
    {
        if(rb.velocity.x > 0 && rb.velocity.y == 0)
        {
            anim.Play("anim_rightWalk");
        }
        else if (rb.velocity.x < 0 && rb.velocity.y == 0)
        {
            anim.Play("anim_leftWalk");
        }
        else if (rb.velocity.x == 0 && rb.velocity.y < 0)
        {
            anim.Play("anim_forwardWalk");
        }
        else if (rb.velocity.x == 0 && rb.velocity.y > 0)
        {
            anim.Play("anim_backwardWalk");
        }
        else
        {
            anim.Play("anim_idle");
        }

    }

}

