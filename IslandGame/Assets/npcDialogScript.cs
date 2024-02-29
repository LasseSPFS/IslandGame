using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npcDialogScript : MonoBehaviour 
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject forts�t;
    public GameObject decline;
    public GameObject accept;

    string[] activeDialog;
    public string[] dialogue;
    public string[] declinAnswers;
    public string[] acceptAnswers;
    public string[] answer1;
    public string[] answer2;
    private int index;
    public float wordSpeed;
    public bool isPlayerClose;
    public bool answerUpcoming;
    days _days; 


    // Days er kaldt fra klassen days, der styrer, hvorlangt i spillet man er. Spillet starter p� 1 og slutter p� 20
    void Start()
    {
        _days = GameObject.Find("DayManager").GetComponent<days>();
        dialogPanel.SetActive(false);
        activeDialog = dialogue;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (dialogText.text == activeDialog[index])
        {
            forts�t.SetActive(true);
        }
        else
        {
            Debug.Log(dialogText.text + " :dialog text | active dialog:" + activeDialog[index]);

        }
    }
    //Funktion der k�res, n�r der klikkes p� forts�t knappen 
    public void NextLine()
    {
        forts�t.SetActive(false);
        //Hvis det ikke er sidste textbox i dialogen S�tter vi teksten til ingenting og fylder den ud igen ved at starte en coroutine ellers k�re den zero tekst.
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

    //Bruges til at fjerne dialogboxen og g�re den klar til n�ste gang den skal bruges
    public void zeroText()
    {
        if(answerUpcoming == false)
        {
            if (dialogText != null)
            {
                dialogText.text = "";
            }
            index = 0;
            dialogPanel.SetActive(false);
        }
        
    }

    //kaldes n�r spilleren skal lave et valg s�tter valg knapperne aktive og tager scar mulighederne fra array af svar.
    public void choice()
    {
        if (dialogText.text == activeDialog[index])
        {
            decline.SetActive(true);
            accept.SetActive(true);
            decline.GetComponentInChildren<TMP_Text>().text = answer1[0];
            accept.GetComponentInChildren<TMP_Text>().text = answer2[0];

        }
    }

    // Hvad der k�res n�r du klikker accept
    public void acceptAnswer()
    {
        activeDialog = acceptAnswers;
        
        NextLine();
        dialogPanel.SetActive(true);
        forts�t.SetActive(false);
        decline.SetActive(false);
        accept.SetActive(false);

        answerUpcoming = false;
    }


    //skriver hvad der st�r i dialogen ud i tekstfeltet et bogstav afgangen 
    IEnumerator Typing( string[] dialog)
    {
        foreach (char Bogstav in dialog[index].ToCharArray())
        {
            if (isPlayerClose && dialogPanel.activeInHierarchy)
            {
                dialogText.text += Bogstav;
                yield return new WaitForSeconds(wordSpeed);
                if (dialog[index].Contains("@"))
                {
                    //answerUpcoming = true;
                    dialog[index].Replace("@", "");
                }
            }
            else
            {
                break;
            }
        }
    }

    //Hvis spilleren kommer t�t 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Du t�t");
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