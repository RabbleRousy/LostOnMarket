using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixtape : MonoBehaviour
{
    [SerializeField] private TraderData traderData;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        Pickup();
    }

    private void Pickup()
    {
        if (!traderData.isSpeakingGibberish) return;
        
        // TODO: fancy stuff, unlock animation etc.
        
        traderData.isSpeakingGibberish = false;
        gameObject.SetActive(false);
    }
}
