using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class activityCaller : MonoBehaviour
{

    [Header("Refferences")]
    public GameObject playerUI;
    public GameObject surviveValg;
    public GameObject npcUI;
    public TMP_Text playerTekst;
    public Image timeSlur;
    public GameObject end;
    public TextMeshProUGUI eTex;


    Inventory _inventory;
    days _days;
    [Header("NPC'er")]
    public npcDialogScript eng;
    public npcDialogScript Træ;
    public npcDialogScript dre;
    public npcDialogScript skr;
    public npcDialogScript præ;
    [Header("Bools")]
    public bool donePicking = false;
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
        
        playerTekst.text = "Kaptajn og jeg arbejde sammen jeg tror ikke han kan lige mig";
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
        if (Træ.NPCLevel == 3)
        {
            Debug.Log("NPC #");
            ending();
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
            _inventory.chooseSnor = true;
            playerTekst.text = "Mig og Emma snakkede om mine oplevelser på øen indtil videre";

        }
    }
    public void hangoutSkræderStof()
    {
        if (skr.NPCLevel == 0)
        {
            playerTekst.text = "Mig og Emma snakkede om mine oplevelser på øen indtil videre";
        }
        if (skr.NPCLevel == 1)
        {
            playerTekst.text = "Jeg gav Emma arbejds ro";
        }
        if (skr.NPCLevel == 2)
        {
            playerTekst.text = "Emma har gjordt sit arbejde, nu mangles bare fundementet og så har jeg en båd";
            _inventory.gotBinding = true;
        }
    }
    public void hangoutPræst()
    {
        if(præ.NPCLevel == 0)
        {
            if (dre.NPCLevel == 1 && dre.npcLockedUntilItem)
            {
                dre.npcLockedUntilItem = false;
                playerTekst.text = "Jeg bad sammen med Abraham og aftalte at snakke med ham om hulen næste gang";
                præ.NPCLevel++;
            }
            else
            {
                playerTekst.text = "Jeg bad sammen med Abraham, jeg føler at guds frelse er nærmmere";
                præ.NPCLevel = 0;
            }
        }
        else if (præ.NPCLevel == 1)
        {
            playerTekst.text = "Vi alle 3 udforskede hulen og bad om hjælp fra Gud i den ";
            dre.NPCLevel++;
            dre.npcLockedUntilItem = false;
        }
        else if (præ.NPCLevel == 2)
        {
            playerTekst.text = "Jeg bad sammen med Abraham, dette er guds vilje";
            if(gotMirror)
            {
                præ.npcLockedUntilItem = false;
                præ.NPCLevel++;
            }
        }
        else if(præ.NPCLevel == 3)
        {
            Debug.Log("be");

            playerTekst.text = "Jeg bad sammen med Abraham, dette er guds vilje";  
            if(gotMirror)
            {
                Debug.Log("gotMirror");
                playerTekst.text = "Vi fandt øens højeste punkt og bøjede solens stråler men gud svaret os ikke";
                præ.NPCLevel++;
            }
        }
        else if (præ.NPCLevel >= 4)
        {
            playerTekst.text = "Vi fandt øens højeste punkt og bøjede solens stråler men gud svaret os ikke";            
            dre.npcLockedUntilItem = false;
        }
    }
    public void hangoutDreng()
    {
        if (dre.NPCLevel == 0)
        {
            if(skr.NPCLevel == 1 && _inventory.chooseSnor)
            {
                playerTekst.text = "Mig og Tomas fandt pinde i form af strikke pinde mens vi legede. Jeg beholdte dem til Emma";
                skr.npcLockedUntilItem = false;
                skr.lvlOne[0] = "Giv mig en dag til at flette noget snor sammen.€";
            }
            else if (skr.NPCLevel == 1 && _inventory.chooseSnor == false)
            {
                playerTekst.text = "Mig og Tomas fandt en masse edderkopper og lavede garnnøjler ud af spindende mens vi legede. Jeg beholdte dem til Emma";
                skr.npcLockedUntilItem = false;
                skr.lvlOne[0] = "Giv mig en dag til at ræde det ud og lave det til noget brugbart€";
            }   
            else
            {
                playerTekst.text = "Jeg legede i skoven med Tomas";
            }
        }
        if(dre.NPCLevel == 2)
        {
            playerTekst.text = "Mig og Tom arbejdede sammen om at forberede beskeden og sendte den ud i havet håbefulde om, at nogen vil finde den.";
        }
        if (dre.NPCLevel == 3)
        {
            playerTekst.text = "Jeg blev givet et spejl af Tom og burde snakke med Abraham om det";
            præ.npcLockedUntilItem = false;
            gotMirror = true;
            if(præ.NPCLevel == 3)
            {
                præ.NPCLevel++;
            }
        }
        if (dre.NPCLevel == 4)
        {
            ending();
        }

    }
    public void hangouteng()
    {
        if(eng.NPCLevel == 0)
        {
            playerTekst.text = "Vi fandt en lighter sammen og havde det sjovt.";
        }
        else if (eng.NPCLevel == 1)
        {
            playerTekst.text = "Jeg hjælp med at samle flyet";
        }
        else if (eng.NPCLevel == 2)
        {
            playerTekst.text = "Jeg fortæller ingenør hvem";
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
            Debug.Log("ending");
            ending();
            yield break;
        }

    }
    public void ending()
    {

        end.SetActive(true);
        if (survivors.Count == 2 && survivors[0] == "Cid" || survivors[1] == "Cid")
        {
            eTex.text = "Vi forlod øen Mig " + survivors[0] + " Micheal " + survivors[1] + " og fik kaldt redning ud på øen, hvor alle blev reddet";
        }
        else if(survivors.Count == 2)
        {
            eTex.text = "Vi prøvede at forlade øen, men da ingen af os kunne flyve styrtede flyet efter 5 min";
        }
        //Dreng og præst ending
        if (gotMirror && præ.NPCLevel >= 4)
        {
            eTex.text = "Vi har gjort alt, hvad vi kunne, men det var ikke nok. Vi er fanget her, men vi er ikke alene, Gud vil altid være med os";
        }

        //Skovhugger og skrædder ending med snor
        if (_inventory.chooseSnor && Træ.NPCLevel >= 4)
        {
            eTex.text = "I sejlede i 2 dage inden i stødte på et fragtskib, der samlede jer op. Du forled øen levende og med et uforglemmeligt bånd til Emma og Chris";
        }
        //Skovhugger og skrædder ending med stof
        else if (!_inventory.chooseSnor && Træ.NPCLevel >= 4)
        {
            eTex.text = "I sejlede i 1 dag inden skibbet gik i 2 da stoffet ikke kunne holde flåden sammen. Dig og Emma kom med den ene, Chris den anden. Emma og dig stødte på et fragt skib en dag senere og kom hjem, men hørte aldrig fra Chris igen"; 
        }
        eTex.text = "Du blev slået ihjel af Cid i nattens mørke, han var utilfreds med, hvor langsom du var";
        Debug.Log("ggg");

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
