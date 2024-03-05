using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    days _days;
    // Start is called before the first frame update
    void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void on

    public void OnCollisionStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bed")
        {
            Debug.Log("godnat");
            if (Input.GetKey(KeyCode.E))
            {
                _days.progresstime();
            }
        }
     
    }
}
