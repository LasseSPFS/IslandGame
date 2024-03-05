using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npcDialogScript : MonoBehaviour 
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject fortsæt;
    public GameObject decline;
    public GameObject accept;

    string[] activeDialog;
    public string[] dialogue;
    public string[] declinAnswers;
    public string[] acceptAnswers;
    public string[] answer1;
    public string[] answer2;
    private int index;
    private int answer;
    public float wordSpeed;
    public bool isPlayerClose;
    days _days; 


    // Days er kaldt fra klassen days, der styrer, hvorlangt i spillet man er. Spillet starter på 1 og slutter på 20
    void Start()
    {
     
        dialogPanel.SetActive(false);   _days = GameObject.Find("DayManager").GetComponent<days>();
        dialogText.text = "";
        answer = 0;
        activeDialog = dialogue;
    }

    // Update is called once per frame
    void Update()
    {
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

              StartCoroutine(Typing(activeDialog));     
            }
        }
        //Når teksten er færdig med at blive skrevet, altså når Coroutine har kørt færdig kommer fortsæt knappen
        if (dialogText.text == activeDialog[index])
        {
            fortsæt.SetActive(true);
        }
    }
    //Funktion der køres, når der klikkes på fortsæt knappen 
    public void NextLine()
    {
        fortsæt.SetActive(false);
        //Hvis det ikke er sidste textbox i dialogen Sætter vi teksten til ingenting og fylder den ud igen ved at starte en coroutine ellers køre den zero tekst.
        if (index < activeDialog.Length - 1)
        {
            index++;
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
    }

    //kaldes når spilleren skal lave et valg sætter valg knapperne aktive og tager scar mulighederne fra array af svar.
    public void choice()
    {
        decline.SetActive(true);
        accept.SetActive(true);
        decline.GetComponentInChildren<TMP_Text>().text = answer1[answer];
        accept.GetComponentInChildren<TMP_Text>().text = answer2[answer];
        answer++;
    }

    // Hvad der køres når du klikker accept
    public void acceptAnswer()
    {

        activeDialog = acceptAnswers;
        decline.SetActive(false);
        accept.SetActive(false);
        fortsæt.SetActive(false);
        index = 0;
        dialogText.text = "";
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


    //skriver hvad der står i dialogen ud i tekstfeltet et bogstav afgangen 
    IEnumerator Typing( string[] dialog)
    {
        foreach (char Bogstav in dialog[index].ToCharArray())
        {
            if (isPlayerClose && dialogPanel.activeInHierarchy)
            {
                if (Bogstav.ToString() == "@")
                {
                    choice();
                }
                else
                {
                    dialogText.text += Bogstav;
                }
                
                yield return new WaitForSeconds(wordSpeed);
                
            }
            else
            {
                Debug.Log("Breaking");
                break;
            }
        }
    }

    //Hvis spilleren kommer tæt 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Du tæt");
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