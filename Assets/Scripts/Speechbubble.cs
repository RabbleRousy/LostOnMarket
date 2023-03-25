using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Speechbubble : MonoBehaviour
{
    [SerializeField] private Trader correspondingTrader;

    private void Start()
    {
        var anim = GetComponent<Animator>();
        var text = GetComponentInChildren<TextMeshProUGUI>();
        correspondingTrader.data.OnSpeakingChanged += () =>
        {
            anim.SetTrigger("Show");
            text.text = correspondingTrader.data.itemForSale + "! " +
                        correspondingTrader.data.itemForSale + "!";
        };
        text.text = correspondingTrader.data.itemGibberish + "! " +
                    correspondingTrader.data.itemGibberish + "!";

        StartCoroutine(WaitThenAnimate());
    }

    private IEnumerator WaitThenAnimate()
    {
        var anim = GetComponent<Animator>();
        anim.enabled = false;
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        yield return new WaitForSeconds(rnd.Next(5));
        anim.enabled = true;
    }
}
