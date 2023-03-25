using System;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "TraderData")]
public class TraderData : ScriptableObject
{
    private bool isSpeakingGibberish = true;
    public EventReference gibberishSound;
    public EventReference translatedSound;
    public string itemForSale;
    public string itemGibberish;

    public event Action OnSpeakingChanged;

    public bool IsSpeakingGibberish
    {
        get => isSpeakingGibberish;
        set
        {
            isSpeakingGibberish = value;
            OnSpeakingChanged?.Invoke();
        }
    }
}
