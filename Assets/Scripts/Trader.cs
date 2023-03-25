using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trader : MonoBehaviour
{
    [SerializeField] private string traderDataFolder;
    [SerializeField] private GameObject E;

    public TraderData data;

    private void Awake()
    {
        var availableTraderData = GetAllInstancesInFolder<TraderData>(traderDataFolder).ToList();

        int index = Random.Range(0, availableTraderData.Count);

        data = availableTraderData[index];
        availableTraderData.RemoveAt(index);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (data.isSpeakingGibberish) return;
        
        E.SetActive(true);
        col.GetComponent<Inventory>().currentTrader = this;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        
        E.SetActive(false);
        col.GetComponent<Inventory>().currentTrader = null;
    }

    public static T[] GetAllInstancesInFolder<T>(string folderPath) where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(T).Name, new[] { folderPath }); 
        T[] a = new T[guids.Length];
        for(int i =0;i<guids.Length;i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }
 
        return a;
 
    }
}
