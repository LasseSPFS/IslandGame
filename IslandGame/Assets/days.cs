using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class days : MonoBehaviour
{
    public GameObject player;
    public GameObject tænkebobbel;
    public int timeslot = 1;
    public Image timeSlur;
    public GameObject playerUI;
    public GameObject surviveValg;
    public TextMeshProUGUI tidOgTid;
    public int dag;
    public NPCDagManager npcMan;
    public Button sleep;
    public activityCaller actCal;
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
            npcMan.secondDay();
        }
    }

    public void daySwitch()
    {
        progresstime();
        StartCoroutine(wait());
        npcMan.secondDay();
        if(itsMorning() && dag == 10)
        {
            actCal.ending();
        }
    }
    public void progresstime()
    {
        timeslot += 1;
        if (timeslot % 3 == 1)
        {
            dag++;

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
        else if (itsDay())
        {
            tidOgTid.text = "Dag: " + dag + "\n" + "Tid på dagen:" + "\n" + "Middag";
        }
        else if (itsEvening())
        {
            tidOgTid.text = "Dag: " + dag + "\n" + "Tid på dagen:" + "\n" + "Aften";
        }
        else
        {
            tidOgTid.text = "wori,ig";
        }
    }

   
    IEnumerator wait()
    {
        timeSlur.color = new Color32(0, 0, 0, 255);
        sleep.gameObject.SetActive(false);
        player.GetComponent<movement>().allowedToMove = false;
        if(player.GetComponent<Player>().inBed  == false)
        {
            tænkebobbel.SetActive(true);
        }      
        player.transform.position = new Vector3(25.08f, -9.11f, -0.3471513f);
        yield return new WaitForSeconds(4);
        tænkebobbel.SetActive(false);
        if (player.GetComponent<Player>().inBed == true)
        {
            sleep.gameObject.SetActive(true);
        }
        player.GetComponent<movement>().allowedToMove = true;
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
