using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private float interval = 5;
    private TraderData traderData;
    private StudioEventEmitter _emitter;

    private void Awake()
    {
        _emitter = GetComponent<StudioEventEmitter>();

    }

    private void Start()
    {
        traderData = GetComponent<SelectTrader>().trader;
        _emitter.EventReference = traderData.gibberishSound;
        InvokeRepeating("Play",interval,interval);
    }

    private void Update()
    {
        if (!traderData.isSpeakingGibberish)
        {
            _emitter.EventReference = traderData.translatedSound;
        }
    }

    private void Play()
    {
        _emitter.Play();
    }
}
