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
    public npcDialogScript Tr�;
    public npcDialogScript dre;
    public npcDialogScript skr;
    public Image timeSlur;
    public GameObject playerUI;
    public GameObject surviveValg;
    Inventory _inventory;
    days _days;
    [Header("Bools")]
    public bool donePicking = false;
    public bool chooseSnor = false;
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
    public void hangoutTr�()
    {
        if(Tr�.NPCLevel == 0)
        {
            playerTekst.text = "Jeg burde lede efter Chris �kse";
        }
        if (Tr�.NPCLevel == 1)
        {
            playerTekst.text = "Mig og chris f�ldede en masse palmer og lavede planker";
            _inventory.didIfellALotOfTrees = true;
        }
    }
    public void hangoutTr�Decline()
    {
        if (Tr�.NPCLevel == 1)
        {
            playerTekst.text = "Mig og chris f�ldede f� palmer og lavede planker";
        }
    }

    public void hangoutSkr�derSnor()
    {
        if (skr.NPCLevel == 0)
        {
            chooseSnor = true;
            playerTekst.text = "Du brugte noget tid p� at snakke med Emma om jeres plan";
        }
    }
    public void hangoutSkr�derStof()
    {
        if (skr.NPCLevel == 0)
        {
            playerTekst.text = "Du brugte noget tid p� at snakke med Emma om jeres plan";
        }
        if (skr.NPCLevel == 1)
        {
            playerTekst.text = "Du gav Emma arbejds ro";
        }
        if (skr.NPCLevel == 2)
        {
            playerTekst.text = "Emma har gjordt sit arbejde, nu mangles bare fundementet og s� har du en b�d";
            _inventory.gotBinding = true;
        }
    }
    public void hangoutDreng()
    {
        if (dre.NPCLevel == 0)
        {
            if(skr.NPCLevel == 1 && chooseSnor)
            {
                playerTekst.text = "Dig og Tomas fandt pinde i form af strikke pinde mens i legede. Du beholdte dem til Emma";
                skr.npcLockedUntilItem = false;
                skr.middag2[0] = "Giv mig en dag til at flette noget snor sammen.�";
            }
            else if (skr.NPCLevel == 1 && chooseSnor == false)
            {
                playerTekst.text = "Dig og Tomas fandt en masse edderkopper og lavede garnn�jler ud af spindende mens i legede. Du beholdte dem til Emma";
                skr.npcLockedUntilItem = false;
                skr.middag2[0] = "Giv mig en dag til at r�de det ud og lave det til noget brugbart�";
            }
            else
            {
                playerTekst.text = "Du legede med tom";
            }

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
            playerTekst.text = "Jeg fort�ller ingene�r hvem";
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
