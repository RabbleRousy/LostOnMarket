using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trader : MonoBehaviour
{
    [SerializeField] private GameObject E;

    public TraderData data;

    private void Start()
    {
        data.IsSpeakingGibberish = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (data.IsSpeakingGibberish) return;
        
        E.SetActive(true);
        col.GetComponent<Inventory>().currentTrader = this;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        E.SetActive(false);
        col.GetComponent<Inventory>().currentTrader = null;
    }
}
