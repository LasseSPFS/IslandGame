using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class days : MonoBehaviour
{
    public int timeslot = 1;
    public Image timeSlur;
    // Start is called before the first frame update
    void Start()
    {
        timeSlur.color = new Color(255, 255, 255, 0);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            progresstime();
        }
    }

    public void progresstime()
    {
        timeslot += 1;
        if(timeslot % 3 == 1)
        {
            timeSlur.color = new Color32(255,255,255,0);
        }
        else if(timeslot % 3 == 2)
        {
            timeSlur.color = new Color32(255, 197, 42, 20);
        }
        else if (timeslot % 3 == 0)
        {
            timeSlur.color = new Color32(0, 0, 0, 210);
        }
    }
}
