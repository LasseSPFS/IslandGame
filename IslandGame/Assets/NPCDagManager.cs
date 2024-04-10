using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDagManager : MonoBehaviour
{
    [HideInInspector] public days _days;
    // Start is called before the first frame update
    void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
    }
    public void secondDay()
    {
        
        if(_days.timeslot > 2)
        {
            if(_days.itsDay() == true || _days.itsMorning())
            {
                foreach (Transform child in transform)
                {
                    if(_days.itsMorning())
                    {
                        child.gameObject.GetComponent<npcDialogScript>().actDoneForTheDay = false;
                    }
                    if(child.gameObject.GetComponent<npcDialogScript>().lvlZero[0] != null && child.gameObject.GetComponent<npcDialogScript>().NPCLevel == 0 )
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().lvlZero;
                    }
                    else if (child.gameObject.GetComponent<npcDialogScript>().NPCLevel == 1)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().lvlOne;
                    }
                    else if (child.gameObject.GetComponent<npcDialogScript>().NPCLevel == 2)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().lvlTwo;
                    }
                    else if (child.gameObject.GetComponent<npcDialogScript>().NPCLevel == 3)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().lvlTre;
                    }
                    else if (child.gameObject.GetComponent<npcDialogScript>().NPCLevel >= 4)
                    {
                        child.gameObject.GetComponent<npcDialogScript>().activeDialog = child.gameObject.GetComponent<npcDialogScript>().lvlFire;
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
        }

    }
}
