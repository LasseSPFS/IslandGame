using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npcDialogScript : MonoBehaviour
{

    [Header ("Acces objects")]
    days _days;
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject PlayerdialogPanel;
    public TextMeshProUGUI PlayerdialogText;
    public GameObject forts�t;
    public GameObject decline;
    public GameObject accept;
    public GameObject E;
    
    [Space(50)]
    [Header("Dialog")]
    [HideInInspector] public string[] activeDialog;
    public string[] dag1;
    public string[] lvlZero;
    public string[] lvlOne;
    public string[] lvlTwo;
    public string[] lvlTre;
    public string[] lvlFire;
    public string[] aften;
    public string[] declinAnswers;
    public string[] acceptAnswers;
    public string[] answer1;
    public string[] answer2; 
    public string[] done;
    public string[] waitingForItem;

    [Header ("V�rdier")]
    public Vector2[] locations;
    private int index;
    public int NPCLevel;
    public float wordSpeed;

    [Header ("If")]
    private bool isPlayerClose;
    private bool activatedActivity;
    public bool actDoneForTheDay;
    public bool npcLockedUntilItem;


    // Days er kaldt fra klassen days, der styrer, hvorlangt i spillet man er. Spillet starter p� 1 og slutter p� 20
    void Start()
    {
        dialogPanel.SetActive(false);
        _days = GameObject.Find("DayManager").GetComponent<days>();
        dialogText.text = "";
        activeDialog = dag1;
    }

    // Update is called once per frame
    void Update()
    {
        if (npcLockedUntilItem)
        {           
            activeDialog = waitingForItem;
        }
        else if (actDoneForTheDay)
        {
            activeDialog = done;
        }

        //tjekker om spilleren er t�t p� NPC'en og om spilleren klikke E
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose && dialogPanel.activeInHierarchy == false)
        {
                dialogPanel.SetActive(true);
                forts�t.SetActive(false);
                decline.SetActive(false);
                accept.SetActive(false);
                StartCoroutine(Typing(activeDialog));
        }
        if(isPlayerClose && dialogPanel.activeInHierarchy == false)
        {
            E.SetActive(true);
            E.transform.localPosition = new Vector3(0, (float)0.3, 0);
        }
        else
        {
            E.SetActive(false);
        }

        //N�r teksten er f�rdig med at blive skrevet, alts� n�r Coroutine har k�rt f�rdig kommer forts�t knappen
        if (dialogText.text == activeDialog[index] && activeDialog != declinAnswers && accept.gameObject.activeInHierarchy == false)
        {
            forts�t.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
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
                else if (Bogstav.ToString() == "^")
                {                   
                    activatedActivity = true;
                    //StartCoroutine(wait());
                }
                else if (Bogstav.ToString() == "#")
                {
                    index = 0;
                    npcLockedUntilItem = true;   
                    
                }
                else if (Bogstav.ToString() == "�")
                {

                    NPCLevel++;
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