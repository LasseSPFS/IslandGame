using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("NPC'er")]
    public npcDialogScript tr�;
    [Header("Items")]
    public bool gotAxe;

    [Header("Items sprite")]
    public GameObject axe;


    [Header("Changed sprite")]
    public Sprite newAxe;

    [Header("Decissions")]
    public bool didIfellALotOfTrees;
   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == axe && tr�.NPCLevel == 1)
        {
            axe.GetComponent<SpriteRenderer>().sprite = newAxe;
            gotAxe = true;
            tr�.npcLockedUntilItem = false;

        }

    }
}
