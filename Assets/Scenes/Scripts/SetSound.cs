using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    [SerializeField] private TraderData traderData;
    private StudioEventEmitter _emitter;

    private void Awake()
    {
        _emitter = GetComponent<StudioEventEmitter>();
        _emitter.EventReference = traderData.gibberishSound;
    }

    private void Update()
    {
        if (!traderData.isSpeakingGibberish)
        {
            _emitter.EventReference = traderData.translatedSound;
        }
    }
}
