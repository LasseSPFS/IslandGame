using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npcDialogScript : MonoBehaviour
{

    [Header ("Acces objects")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject PlayerdialogPanel;
    public TextMeshProUGUI PlayerdialogText;
    public GameObject forts�t;
    public GameObject decline;
    public GameObject accept;
    
    [Space(50)]
    [Header("Dialog")]
    [HideInInspector] public string[] activeDialog;
    public string[] dag1;
    public string[] middag;
    public string[] aften;
    public string[] morgen;
    public string[] middag2;
    public string[] aften2;
    public string[] morgen2;
    public string[] middag3;
    public string[] aften3;
    public string[] morgen3;
    public string[] middag4;
    public string[] aften4;
    public string[] morgen4;
    public string[] activity1;
    public string[] declinAnswers;
    public string[] acceptAnswers;
    public string[] answer1;
    public string[] answer2; 
    public string[] answer3;
    public string[] done;
    public string[] waitingForItem;

    [Header ("V�rdier")]
    public Vector2[] locations;
    private int index;
    private int answer = 0;
    private int acceptIndex = 0;
    private int actNumber = 1;
    public int NPCLevel;
    public float wordSpeed;

    [Header ("If")]
    private bool isPlayerClose;
    private bool activatedActivity;
    public bool isMorningAndDaySame;
    public bool activateAct1;
    public bool actDoneForTheDay;
    public bool npcLockedUntilItem;
    days _days;


    // Days er kaldt fra klassen days, der styrer, hvorlangt i spillet man er. Spillet starter p� 1 og slutter p� 20
    void Start()
    {
        dialogPanel.SetActive(false);
        _days = GameObject.Find("DayManager").GetComponent<days>();
        dialogText.text = "";
        activeDialog = dag1;
        if (isMorningAndDaySame)
        {
            morgen = middag;
            morgen2 = middag2;
            middag3 = morgen3;
            middag4 = morgen4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (actDoneForTheDay)
        {
            activeDialog = done;
        }
        else if(npcLockedUntilItem)
        {
            activeDialog = waitingForItem;
        }
        //tjekker om spilleren er t�t p� NPC'en og om spilleren klikke E
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose)
        {
            //Hvis textbox allerede er aktivt s� deaktivere det
            if (dialogPanel.activeInHierarchy)
            {
                zeroText();
            }
            
            //Ellers aktiver dialogpanel og begynd Coroutine 
            else
            {
                dialogPanel.SetActive(true);
                forts�t.SetActive(false);
                decline.SetActive(false);
                accept.SetActive(false);
                StartCoroutine(Typing(activeDialog));
            }
        }
        //N�r teksten er f�rdig med at blive skrevet, alts� n�r Coroutine har k�rt f�rdig kommer forts�t knappen
        if (dialogText.text == activeDialog[index] && activeDialog != declinAnswers)
        {
            forts�t.SetActive(true);
        }
        else 
        if (_days.itsMorning() == true)
        {
            transform.localPosition = locations[0];
        }
        else if (_days.itsDay() == true)
        {
            transform.localPosition = locations[1];
        }
        else if (_days.itsEvening() == true)
        {
            transform.localPosition = locations[2];
        }

    }
    //Funktion der k�res, n�r der klikkes p� forts�t knappen 
    public void NextLine()
    {
        forts�t.SetActive(false);
        //Hvis det ikke er sidste textbox i dialogen S�tter vi teksten til ingenting og fylder den ud igen ved at starte en coroutine ellers k�re den zero tekst.
        if (index < activeDialog.Length - 1)
        {
            if(activatedActivity)
            {              
                activatedActivity = false;
            }
            else
            {
                index++;
            }
               
            dialogText.text = "";
            StartCoroutine(Typing(activeDialog));
        }
        else
        {
            zeroText();
        }
    }

    //Bruges til at fjerne dialogboxen og g�re den klar til n�ste gang den skal bruges
    public void zeroText()
    {
        dialogText.text = "";
        index = 0;

        dialogPanel.SetActive(false);
        
        if (activatedActivity == true)
        {
            PlayerdialogPanel.SetActive(true);
            _days.daySwitch();
            activatedActivity = false;
            actDoneForTheDay = true;
        }
        
    }

    //kaldes n�r spilleren skal lave et valg s�tter valg knapperne aktive og tager scar mulighederne fra array af svar.
    public void choice()
    {
        decline.SetActive(true);
        accept.SetActive(true);
        decline.GetComponentInChildren<TMP_Text>().text = answer1[NPCLevel];
        accept.GetComponentInChildren<TMP_Text>().text = answer2[NPCLevel];
    }

    // Hvad der k�res n�r du klikker accept
    public void acceptAnswer()
    {
        decline.SetActive(false);
        accept.SetActive(false);
        forts�t.SetActive(false);
        index = NPCLevel;
        dialogText.text = "";
        activeDialog = acceptAnswers;
        StartCoroutine(Typing(activeDialog));
    }

    public void declineAnswer()
    {
        decline.SetActive(false);
        accept.SetActive(false);
        forts�t.SetActive(false);
        index = NPCLevel;
        dialogText.text = "";
        activeDialog = declinAnswers;
        StartCoroutine(Typing(activeDialog));
    }

    public string[] whichAct(int act)
    {
        if (act == 1)
        {
            actNumber++;
            return activity1;
        }
       
        else return activeDialog;
    }

    public void actRunner()
    {
        forts�t.SetActive(true);
        index = 0;
        activatedActivity = true;
        activeDialog = whichAct(actNumber);
    }
    

    //skriver hvad der st�r i dialogen ud i tekstfeltet et bogstav afgangen 
    IEnumerator Typing(string[] dialog)
    {
        foreach (char Bogstav in dialog[index].ToCharArray())
        {
            if (isPlayerClose && dialogPanel.activeInHierarchy)
            {
              
                if (Bogstav.ToString() == "@")
                {
                    choice();
                }             
                else if (Bogstav.ToString() == "$")
                {
                    NPCLevel += 1;
                    activatedActivity = true;
                    //StartCoroutine(wait());
                }
                else if (Bogstav.ToString() == "#")
                {
                    npcLockedUntilItem = true;              
                }
                else if (Bogstav.ToString() == "�")
                {

                    actRunner();
                }
                else
                {
                    dialogText.text += Bogstav;
                }

                yield return new WaitForSeconds(wordSpeed);

            }
            else
            {
                break;
            }
        }
    }
    //Make it not run when selecting survivors.

    /*
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        dialogPanel.SetActive(false);
        PlayerdialogPanel.SetActive(true);
        Debug.Log(activeDialog[0]);
        _days.daySwitch();
        index = NPCLevel - 1;
        activatedActivity = false;
        actDoneForTheDay = true;
        activeDialog = done;
        Debug.Log(activeDialog[0]);
        yield break;
    }

    */
    //Hvis spilleren kommer t�t 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = true;
        }

    }

    //Hvis spilleren forlader NPC'en 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = false;
            zeroText();
        }
    }
}