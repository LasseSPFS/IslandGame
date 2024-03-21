using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npcDialogScript : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject PlayerdialogPanel;
    public TextMeshProUGUI PlayerdialogText;
    public GameObject fortsæt;
    public GameObject decline;
    public GameObject accept;

    public string[] playerActivityDeskription;
    [HideInInspector] public string[] activeDialog;
    public string[] dag1;
    public string[] middag;
    public string[] aften;
    public string[] morgen;
    public string[] middag2;
    public string[] aften2;
    public string[] morgen2;
    public string[] activity1;
    public string[] activity2;
    public string[] declinAnswers;
    public string[] acceptAnswers;
    public string[] answer1;
    public string[] answer2;
    public string[] done;
    public Vector2[] locations;
    private int index;
    private int answer;
    private int acceptIndex = 0;
    private int actNumber = 1;
    public int NPCLevel;
    public float wordSpeed;
    public bool isPlayerClose;
    public bool isLocked;
    public bool activatedActivity;
    public bool isMorningAndDaySame;
    public bool unusualCon;
    public bool checker;
    public bool activateAct1;
    public bool actDoneForTheDay;
    List<string[]> actList;
    days _days;


    // Days er kaldt fra klassen days, der styrer, hvorlangt i spillet man er. Spillet starter på 1 og slutter på 20
    void Start()
    {
        Debug.Log(actList);
        dialogPanel.SetActive(false);
        _days = GameObject.Find("DayManager").GetComponent<days>();
        dialogText.text = "";
        answer = 0;
        activeDialog = dag1;

        if (isMorningAndDaySame)
        {
            morgen = middag;
            morgen2 = middag2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(checker)
        {

        }
        //tjekker om spilleren er tæt på NPC'en og om spilleren klikke E
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose)
        {

            //Hvis textbox allerede er aktivt så deaktivere det
            if (dialogPanel.activeInHierarchy)
            {
                zeroText();
            }
            
            //Ellers aktiver dialogpanel og begynd Coroutine 
            else
            {
                dialogPanel.SetActive(true);
                fortsæt.SetActive(false);
                decline.SetActive(false);
                accept.SetActive(false);
                if (actDoneForTheDay)
                {                  
                    activeDialog = done;                   
                }
                StartCoroutine(Typing(activeDialog));
            }
        }
        //Når teksten er færdig med at blive skrevet, altså når Coroutine har kørt færdig kommer fortsæt knappen
        if (dialogText.text == activeDialog[index])
        {
            fortsæt.SetActive(true);
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
    //Funktion der køres, når der klikkes på fortsæt knappen 
    public void NextLine()
    {
        fortsæt.SetActive(false);
        //Hvis det ikke er sidste textbox i dialogen Sætter vi teksten til ingenting og fylder den ud igen ved at starte en coroutine ellers køre den zero tekst.
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

    //Bruges til at fjerne dialogboxen og gøre den klar til næste gang den skal bruges
    public void zeroText()
    {
        dialogText.text = "";
        index = 0;

        dialogPanel.SetActive(false);
        if (activatedActivity == true)
        {
            PlayerdialogPanel.SetActive(true);
            PlayerdialogText.text = playerActivityDeskription[NPCLevel - 1];
            _days.daySwitch();
            isLocked = true;
            activatedActivity = false;
            actDoneForTheDay = true;
        }
    }

    //kaldes når spilleren skal lave et valg sætter valg knapperne aktive og tager scar mulighederne fra array af svar.
    public void choice()
    {
        decline.SetActive(true);
        accept.SetActive(true);
        decline.GetComponentInChildren<TMP_Text>().text = answer1[answer];
        accept.GetComponentInChildren<TMP_Text>().text = answer2[answer];
    }

    // Hvad der køres når du klikker accept
    public void acceptAnswer()
    {
        decline.SetActive(false);
        accept.SetActive(false);
        fortsæt.SetActive(false);
        index = acceptIndex;
        dialogText.text = "";
        activeDialog[0] = acceptAnswers[NPCLevel];
        StartCoroutine(Typing(activeDialog));
    }

    public void declineAnswer()
    {
        activeDialog = declinAnswers;
        decline.SetActive(false);
        accept.SetActive(false);
        fortsæt.SetActive(false);
        dialogText.text = "";
        index = 0;
        StartCoroutine(Typing(activeDialog));
    }

    public string[] whichAct(int act)
    {
        Debug.Log(act);
        if (act == 1)
        {
            actNumber++;
            return activity1;
        }
        else if (act == 2)
        {
            actNumber++;
            return activity2;
        }
        else return activeDialog;

    }

    public void actRunner()
    {
        fortsæt.SetActive(true);
        index = 0;
        activatedActivity = true;
        activeDialog = whichAct(actNumber);
    }
    

    //skriver hvad der står i dialogen ud i tekstfeltet et bogstav afgangen 
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
                }
                else if (Bogstav.ToString() == "£")
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

    //Hvis spilleren kommer tæt 
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