using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class activityCaller : MonoBehaviour
{
    public TMP_Text playerTekst;
    public GameObject npcUI;

    public void Start()
    {

    }
    public void hangoutKaptain1()
    {
        playerTekst.text = "I helped out kaptain i feel like we are closer now";
    }
}
