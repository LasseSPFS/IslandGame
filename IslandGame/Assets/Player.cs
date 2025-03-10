using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    days _days;
    public GameObject sleepButton;
    public bool inBed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(inBed);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bed")
        {
            sleepButton.SetActive(true);         
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bed")
        {
            inBed = true;
        }
      
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bed")
        {
            sleepButton.SetActive(false);
        }
        inBed = false;
    }

}
