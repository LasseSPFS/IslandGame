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
    public npcDialogScript dre;
    public npcDialogScript skr;
    public npcDialogScript præ;
    public Image timeSlur;
    public GameObject playerUI;
    public GameObject surviveValg;
    Inventory _inventory;
    days _days;
    [Header("Bools")]
    public bool donePicking = false;
    public bool chooseSnor = false;
    public bool gotMirror = false;
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

    public void hangoutSkræderSnor()
    {
        if (skr.NPCLevel == 0)
        {
            chooseSnor = true;
            playerTekst.text = "Du brugte noget tid på at snakke med Emma om jeres plan";
        }
    }
    public void hangoutSkræderStof()
    {
        if (skr.NPCLevel == 0)
        {
            playerTekst.text = "Du brugte noget tid på at snakke med Emma om jeres plan";
        }
        if (skr.NPCLevel == 1)
        {
            playerTekst.text = "Du gav Emma arbejds ro";
        }
        if (skr.NPCLevel == 2)
        {
            playerTekst.text = "Emma har gjordt sit arbejde, nu mangles bare fundementet og så har du en båd";
            _inventory.gotBinding = true;
        }
    }
    public void hangoutPræst()
    {
        Debug.Log("ran this2");
        if(præ.NPCLevel == 0)
        {
            if (dre.NPCLevel == 1 && dre.npcLockedUntilItem)
            {
                dre.npcLockedUntilItem = false;
                playerTekst.text = "Du bad sammen med Abraham og aftalte at snakke med ham om hulen næste gang";
                præ.NPCLevel++;
            }
            else
            {
                playerTekst.text = "Du bad sammen med Abraham, du føler at guds frelse er nærmmere";
                præ.NPCLevel = 0;
            }
        }
        else if (præ.NPCLevel == 1)
        {
            Debug.Log("Ran this");
            playerTekst.text = "I alle 3 udforskede hulen og bad om hjælp fra Gud i den ";
            dre.NPCLevel++;
            dre.npcLockedUntilItem = false;
        }
        else if (præ.NPCLevel == 2)
        {
            playerTekst.text = "Du bad sammen med Abraham, dette er guds vilje";
            if(gotMirror)
            {
                præ.npcLockedUntilItem = false;
            }
        }
        else if(præ.NPCLevel == 3)
        {
            playerTekst.text = "Vi fandt øens højeste punkt og bøjede solens stråler men gud svaret os ikke";
            dre.npcLockedUntilItem = false;
        }
        else if (præ.NPCLevel == 4)
        {
            playerTekst.text = "Du bad sammen med Abraham, dette er guds vilje ";
            dre.npcLockedUntilItem = false;
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
                skr.middag2[0] = "Giv mig en dag til at flette noget snor sammen.€";
            }
            else if (skr.NPCLevel == 1 && chooseSnor == false)
            {
                playerTekst.text = "Dig og Tomas fandt en masse edderkopper og lavede garnnøjler ud af spindende mens i legede. Du beholdte dem til Emma";
                skr.npcLockedUntilItem = false;
                skr.middag2[0] = "Giv mig en dag til at ræde det ud og lave det til noget brugbart€";
            }   
            else
            {
                playerTekst.text = "Du legede i skoven med Tomas";
            }
        }
        if(dre.NPCLevel == 2)
        {
            playerTekst.text = "Dig og tom arbejder sammen om at forberede beskeden og sender den ud i havet håbefulde om, at nogen vil finde den.";
        }
        if (dre.NPCLevel == 3)
        {
            playerTekst.text = "Jeg blev givet et spejl af Tom og burde snakke med Abraham om det";
            præ.npcLockedUntilItem = false;
            gotMirror = true;
        }
        if (dre.NPCLevel == 4)
        {
            playerTekst.text = "Jeg kan ikke mere. Vi har forsøgt alt, men vi er stadig fanget her.Der kommer aldrig nogen forbi.";
            præ.npcLockedUntilItem = false;
            ending();

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
    public void ending()
    {
        if(dre.NPCLevel == 4 && præ.NPCLevel == 3)
        {
            timeSlur.color = Color.blue;
            playerUI.SetActive(true);
            playerTekst.text = " Vi har gjort alt, hvad vi kunne, men det var ikke nok. Vi er fanget her, men vi er ikke alene, Gud vil altid være med os.";
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
