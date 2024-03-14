using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class days : MonoBehaviour
{
    public int timeslot = 1;
    public Image timeSlur;
    public GameObject playerUI;
    public TextMeshProUGUI tidOgTid;
    int dag;
    // Start is called before the first frame update
    void Start()
    {
        timeSlur.color = new Color(255, 255, 255, 0);
        progresstime();

    }

    void Update()
    {
        clockController();
        if (Input.GetKeyUp(KeyCode.K))
        {
            progresstime();
        }
    }

    public void daySwitch()
    {
        progresstime();
        StartCoroutine(wait());
    }
    public void progresstime()
    {
        timeslot += 1;
        if (timeslot % 3 == 1)
        {
            timeSlur.color = new Color32(255, 255, 255, 0);
        }
        else if (timeslot % 3 == 2)
        {
            timeSlur.color = new Color32(255, 197, 42, 20);
        }
        else if (timeslot % 3 == 0)
        {
            timeSlur.color = new Color32(0, 0, 0, 210);
        }

        //fjern efter sprint 1
        if(timeslot ==7)
        {
            Application.Quit();
        }
    }
     public bool itsMorning()
    {
        if (timeslot % 3 == 1)
        {
            return true;
        }
        else 
        { 
            return false; 
        }
            
    }
    public bool itsDay()
    {
        if (timeslot % 3 == 2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool itsEvening()
    {
        if (timeslot % 3 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void clockController()
    {
        if(itsMorning())
        {
            tidOgTid.text = "Dag: " + dag + "\n" + "Tid på dagen:"+"\n" + "Morgen";
        }
        else
        {
            tidOgTid.text = "wori,ig";
        }
    }

    IEnumerator wait()
    {
        timeSlur.color = new Color32(0, 0, 0, 255);
        yield return new WaitForSeconds(5);
        playerUI.SetActive(false);
        if (timeslot % 3 == 1)
        {
            timeSlur.color = new Color32(255, 255, 255, 0);
        }
        else if (timeslot % 3 == 2)
        {
            timeSlur.color = new Color32(255, 197, 42, 20);
        }
        else if (timeslot % 3 == 0)
        {
            timeSlur.color = new Color32(0, 0, 0, 210);
        }
    }
}
