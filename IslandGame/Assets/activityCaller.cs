using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class activityCaller : MonoBehaviour
{
    public TMP_Text playerTekst;
    public GameObject npcUI;
    [Header("NPC'er")]
    public npcDialogScript eng;
    public npcDialogScript Træ;
    public Image timeSlur;
    public GameObject playerUI;
    public GameObject surviveValg;
    Inventory _inventory;
    days _days;
    public bool donePicking = false;

    [Header("Survivorpicker")]
    public List<string> survivors; 

    public void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
        _inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    public void hangoutKaptain1()
    {
        Debug.Log("ran");
        
        playerTekst.text = "I helped out kaptain i feel like we are closer now";
    }
    public void hangoutTræ()
    {
        if(Træ.NPCLevel == 0)
        {
            playerTekst.text = "Jeg burde lede efter Chris økse";
        }
        if (Træ.NPCLevel == 1)
        {
            playerTekst.text = "Mig og chris fældede en masse palmer og lavede planker";
            _inventory.didIfellALotOfTrees = true;
        }
    }
    public void hangoutTræDecline()
    {
        if (Træ.NPCLevel == 1)
        {
            playerTekst.text = "Mig og chris fældede få palmer og lavede planker";
        }
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
            if (_days.timeslot % 3 == 1)
            {
                timeSlur.color = new Color32(255, 255, 255, 0);
            }
            else if (_days.timeslot % 3 == 2)
            {
                timeSlur.color = new Color32(255, 197, 42, 20);
            }
            else if (_days.timeslot % 3 == 0)
            {
                timeSlur.color = new Color32(0, 0, 0, 210);
            }
            playerUI.SetActive(false);
            surviveValg.SetActive(false);
            yield break;
        }

    }

    public void pickedSurvior(Button button)
    {
        if(survivors.Count < 2)
        {          
            survivors.Add(button.name);
            if (survivors.Count == 2)
            {
                Debug.Log("done");
                donePicking = true;
            }
            Debug.Log(survivors.Count);
        }
    }
}
