using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Vector2 move;
    public Animator anim;
    public bool allowedToMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        whichWayAmILooking();
    }

    private void FixedUpdate()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (allowedToMove == true)
        {
           
            rb.velocity = move.normalized * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
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
        else if (rb.velocity.x < 0 && rb.velocity.y > 0)
        {
            anim.Play("anim_diagUpLeft");
        }
        else if (rb.velocity.x > 0 && rb.velocity.y > 0)
        {
            anim.Play("anim_diagUpRight");
        }
        else if (rb.velocity.x < 0 && rb.velocity.y < 0)
        {
            anim.Play("anim_diagDownLeft");
        }
        else if (rb.velocity.x > 0 && rb.velocity.y < 0)
        {
            anim.Play("anim_diagDownRight");
        }
        else
        {
            anim.Play("anim_idle");
        }

    }
}

