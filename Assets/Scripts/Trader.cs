using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Trader : MonoBehaviour
{
    [SerializeField] private string traderDataFolder;

    public TraderData data;
    
    private static List<int> _traderIndices;

    private void Awake()
    {
        var availableTraderData = GetAllInstancesInFolder<TraderData>(traderDataFolder).ToList();

        int index = Random.Range(0, availableTraderData.Count);

        data = availableTraderData[index];
        availableTraderData.RemoveAt(index);
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
