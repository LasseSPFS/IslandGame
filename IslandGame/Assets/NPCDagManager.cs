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
    }
    public void secondDay()
    {
        if(_days.timeslot != 1)
        {
        if(_days.itsDay() == true)
        {
            foreach (Transform child in transform)
            {
                if(child.gameObject.GetComponent<npcDialogScript>().dialogue2[0].Length != 0)
                {
                    child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().dialogue2;
                }             
            }
        }

        if (_days.itsEvening() == true)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<npcDialogScript>().dialogue3.Length != 0)
                {
                    child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().dialogue3;
                }
            }
        }
            if (_days.itsMorning() == true)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.GetComponent<npcDialogScript>().dialogue4[0].Length != 0)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().dialogue4;
                    }
                }
            }
        }

    }
}
