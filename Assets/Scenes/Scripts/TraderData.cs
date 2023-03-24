using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "TraderData")]
public class TraderData : ScriptableObject
{
    public bool isSpeakingGibberish = true;
    public EventReference gibberishSound;
    public EventReference translatedSound;
}
