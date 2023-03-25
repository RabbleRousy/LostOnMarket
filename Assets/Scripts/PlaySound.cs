using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private TraderData traderData;
    private StudioEventEmitter _emitterGibberish;
    private StudioEventEmitter _emitterTranslated;

    private void Awake()
    {
        _emitterGibberish = GetComponents<StudioEventEmitter>()[0];
        _emitterTranslated = GetComponents<StudioEventEmitter>()[1];

    }

    private void Start()
    {
        traderData = GetComponent<Trader>().data;
        _emitterGibberish.EventReference = traderData.gibberishSound;
        _emitterTranslated.EventReference = traderData.translatedSound;
        Play();

        traderData.OnSpeakingChanged += Play;

    }

    private void Play()
    {
        if (traderData.IsSpeakingGibberish)
        {
            _emitterTranslated.Stop();
            _emitterGibberish.Play();
        }
        else
        {
            _emitterGibberish.Stop();
            _emitterTranslated.Play();
        }
    }
}
