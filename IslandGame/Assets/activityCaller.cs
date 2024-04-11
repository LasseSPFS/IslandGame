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
    public npcDialogScript Tr�;
    public npcDialogScript dre;
    public npcDialogScript skr;
    public npcDialogScript pr�;
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
        if (Tr�.NPCLevel == 3)
        {
            Debug.Log("NPC #");
            ending();
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
            _inventory.chooseSnor = true;
            playerTekst.text = "Mig og Emma snakkede om mine oplevelser p� �en indtil videre";

        }
    }
    public void hangoutSkr�derStof()
    {
        if (skr.NPCLevel == 0)
        {
            playerTekst.text = "Mig og Emma snakkede om mine oplevelser p� �en indtil videre";
        }
        if (skr.NPCLevel == 1)
        {
            playerTekst.text = "Jeg gav Emma arbejds ro";
        }
        if (skr.NPCLevel == 2)
        {
            playerTekst.text = "Emma har gjordt sit arbejde, nu mangles bare fundementet og s� har jeg en b�d";
            _inventory.gotBinding = true;
        }
    }
    public void hangoutPr�st()
    {
        if(pr�.NPCLevel == 0)
        {
            if (dre.NPCLevel == 1 && dre.npcLockedUntilItem)
            {
                dre.npcLockedUntilItem = false;
                playerTekst.text = "Jeg bad sammen med Abraham og aftalte at snakke med ham om hulen n�ste gang";
                pr�.NPCLevel++;
            }
            else
            {
                playerTekst.text = "Jeg bad sammen med Abraham, jeg f�ler at guds frelse er n�rmmere";
                pr�.NPCLevel = 0;
            }
        }
        else if (pr�.NPCLevel == 1)
        {
            playerTekst.text = "Vi alle 3 udforskede hulen og bad om hj�lp fra Gud i den ";
            dre.NPCLevel++;
            dre.npcLockedUntilItem = false;
        }
        else if (pr�.NPCLevel == 2)
        {
            playerTekst.text = "Jeg bad sammen med Abraham, dette er guds vilje";
            if(gotMirror)
            {
                pr�.npcLockedUntilItem = false;
                pr�.NPCLevel++;
            }
        }
        else if(pr�.NPCLevel == 3)
        {
            Debug.Log("be");

            playerTekst.text = "Jeg bad sammen med Abraham, dette er guds vilje";  
            if(gotMirror)
            {
                Debug.Log("gotMirror");
                playerTekst.text = "Vi fandt �ens h�jeste punkt og b�jede solens str�ler men gud svaret os ikke";
                pr�.NPCLevel++;
            }
        }
        else if (pr�.NPCLevel >= 4)
        {
            playerTekst.text = "Vi fandt �ens h�jeste punkt og b�jede solens str�ler men gud svaret os ikke";            
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
                skr.lvlOne[0] = "Giv mig en dag til at flette noget snor sammen.�";
            }
            else if (skr.NPCLevel == 1 && _inventory.chooseSnor == false)
            {
                playerTekst.text = "Mig og Tomas fandt en masse edderkopper og lavede garnn�jler ud af spindende mens vi legede. Jeg beholdte dem til Emma";
                skr.npcLockedUntilItem = false;
                skr.lvlOne[0] = "Giv mig en dag til at r�de det ud og lave det til noget brugbart�";
            }   
            else
            {
                playerTekst.text = "Jeg legede i skoven med Tomas";
            }
        }
        if(dre.NPCLevel == 2)
        {
            playerTekst.text = "Mig og Tom arbejdede sammen om at forberede beskeden og sendte den ud i havet h�befulde om, at nogen vil finde den.";
        }
        if (dre.NPCLevel == 3)
        {
            playerTekst.text = "Jeg blev givet et spejl af Tom og burde snakke med Abraham om det";
            pr�.npcLockedUntilItem = false;
            gotMirror = true;
            if(pr�.NPCLevel == 3)
            {
                pr�.NPCLevel++;
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
            playerTekst.text = "Jeg hj�lp med at samle flyet";
        }
        else if (eng.NPCLevel == 2)
        {
            playerTekst.text = "Jeg fort�ller ingen�r hvem";
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
            eTex.text = "Vi forlod �en Mig " + survivors[0] + " Micheal " + survivors[1] + " og fik kaldt redning ud p� �en, hvor alle blev reddet";
        }
        else if(survivors.Count == 2)
        {
            eTex.text = "Vi pr�vede at forlade �en, men da ingen af os kunne flyve styrtede flyet efter 5 min";
        }
        //Dreng og pr�st ending
        if (gotMirror && pr�.NPCLevel >= 4)
        {
            eTex.text = "Vi har gjort alt, hvad vi kunne, men det var ikke nok. Vi er fanget her, men vi er ikke alene, Gud vil altid v�re med os";
        }

        //Skovhugger og skr�dder ending med snor
        if (_inventory.chooseSnor && Tr�.NPCLevel >= 4)
        {
            eTex.text = "I sejlede i 2 dage inden i st�dte p� et fragtskib, der samlede jer op. Du forled �en levende og med et uforglemmeligt b�nd til Emma og Chris";
        }
        //Skovhugger og skr�dder ending med stof
        else if (!_inventory.chooseSnor && Tr�.NPCLevel >= 4)
        {
            eTex.text = "I sejlede i 1 dag inden skibbet gik i 2 da stoffet ikke kunne holde fl�den sammen. Dig og Emma kom med den ene, Chris den anden. Emma og dig st�dte p� et fragt skib en dag senere og kom hjem, men h�rte aldrig fra Chris igen"; 
        }
        eTex.text = "Du blev sl�et ihjel af Cid i nattens m�rke, han var utilfreds med, hvor langsom du var";
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
