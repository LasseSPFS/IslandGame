using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDagManager : MonoBehaviour
{
    public days _days;
    // Start is called before the first frame update
    void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
    }

    // Update is called once per frame
    void Update()
    {
        secondDay();
    }
    public void secondDay()
    {
        if(_days.timeslot > 2)
        {
            if(_days.itsDay() == true)
            {
                foreach (Transform child in transform)
                {
                    if(child.gameObject.GetComponent<npcDialogScript>().middag[0] != null)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().middag;
                    }             
                }
            }

            if (_days.itsEvening() == true)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.GetComponent<npcDialogScript>().aften != null)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().aften;
                    }
                }
            }
            if (_days.itsMorning() == true)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.GetComponent<npcDialogScript>().morgen[0] != null)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().morgen;
                    }
                }
            }
        }

    }
}
