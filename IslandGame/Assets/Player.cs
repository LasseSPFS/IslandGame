using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
