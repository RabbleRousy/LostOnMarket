using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    [SerializeField] private TraderData[] traders;

    public TraderData data;
    
    private static List<int> _traderIndices;

    private void Awake()
    {
        if (_traderIndices == null)
        {
            _traderIndices = new List<int>();
            for (int i = 0; i < traders.Length; i++)
            {
                _traderIndices.Add(i);
            }
        }
        
        int index = Random.Range(0, _traderIndices.Count);
        _traderIndices.Remove(index);

        data = traders[index];
    }
}