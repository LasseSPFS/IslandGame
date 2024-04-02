using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class activityCaller : MonoBehaviour
{
    public TMP_Text playerTekst;
    public GameObject npcUI;
    public npcDialogScript eng;
    public Image timeSlur;
    public GameObject playerUI;
    public GameObject surviveValg;
    days _days;
    public bool donePicking = false;

    public void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
    }
    public void hangoutKaptain1()
    {
        Debug.Log("ran");
        
        playerTekst.text = "I helped out kaptain i feel like we are closer now";
    }
    public void hangouteng()
    {
        if(eng.NPCLevel == 0)
        {
            playerTekst.text = "I fandt en lighter sammen og havde det sjovt.";
        }
        else if (eng.NPCLevel == 1)
        {
            playerTekst.text = "Noget";
        }
        else if (eng.NPCLevel == 2)
        {
            playerTekst.text = "Jeg fortæller ingeneær hvem";
            StartCoroutine(picksurvivor());
        }

        IEnumerator picksurvivor()
        {
            timeSlur.color = new Color32(0, 0, 0, 255);
            Debug.Log("aktiverre");
            surviveValg.SetActive(true);
            while (!donePicking)
            {
                yield return null;
            }
            Debug.Log("del 2");
            playerUI.SetActive(false);
            surviveValg.SetActive(false);
        }

    }

    public void changeDonePicking()
    {
        donePicking = true;    
    }
}
