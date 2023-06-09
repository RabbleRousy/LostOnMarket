using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixtape : MonoBehaviour
{
    public TraderData traderData;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        Pickup();
    }

    private void Pickup()
    {
        if (!traderData.IsSpeakingGibberish) return;
        
        // TODO: fancy stuff, unlock animation etc.
        
        traderData.IsSpeakingGibberish = false;
        gameObject.SetActive(false);
    }
}
