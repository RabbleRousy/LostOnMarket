using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Inventory : MonoBehaviour
{
    [SerializeField] private string traderDataFolder;
    [SerializeField] private List<ItemObject> currentInventory;

    public Trader currentTrader = null;

    [SerializeField] private Toggle[] shoppingListItems;
    [SerializeField] private TextMeshProUGUI notificationText;

    private void Awake()
    {
        SetUpTradersAndMixtapes();
        SetUpShoppingList();
    }

    private void SetUpTradersAndMixtapes()
    {
        // Get all candidates and shuffle them
        Random random = new Random(Guid.NewGuid().GetHashCode());
        var traderData = GetAllInstancesInFolder<TraderData>(traderDataFolder)
            .ToList().OrderBy(item => random.Next()).ToArray();
        var tradersInScene = FindObjectsOfType<Trader>()
            .ToList().OrderBy(item => random.Next()).ToArray();
        var mixtapesInScene = FindObjectsOfType<Mixtape>()
            .ToList().OrderBy(item => random.Next()).ToArray();

        for (int i = 0; i < 4; i++)
        {
            tradersInScene[i].data = traderData[i];
            mixtapesInScene[i].traderData = traderData[i];
            currentInventory.Add(new ItemObject(traderData[i].itemForSale, 1, 0));
            traderData[i].IsSpeakingGibberish = true;
        }
    }
    
    private void SetUpShoppingList()
    {
        // TODO: Randomly pick some items that are needed
        for (int i = 0; i < 4; i++)
        {
            var label = shoppingListItems[i].GetComponentInChildren<TextMeshProUGUI>();
            var item = currentInventory[i];
            label.SetText(item.name);
        }
    }

    public void Collect(string itemName)
    {
        int index = currentInventory.FindIndex(item => item.name == itemName);
        if (index != -1)
        {
            currentInventory[index].currentAmount += 1;
        }
        else
        {
            // Currently unused
            currentInventory.Add(new ItemObject(itemName, 0, 1));
        }

        StartCoroutine(ShowNotification(itemName));
        CheckShoppingList();
    }

    private IEnumerator ShowNotification(string itemName)
    {
        notificationText.gameObject.SetActive(true);
        notificationText.text = "JUHU! I collected \"" + itemName + "\"!";
        yield return new WaitForSeconds(3f);
        notificationText.gameObject.SetActive(false);
    }

    public bool CheckShoppingList()
    {
        bool check = true;
        for (int i = 0; i < currentInventory.Count; i++)
        {
            var item = currentInventory[i];
            // Item is not required, skip check
            if (item.requiredAmount == 0) continue;

            if (item.currentAmount < item.requiredAmount)
            {
                shoppingListItems[i].SetIsOnWithoutNotify(false);
                check = false;
            }
            else
            {
                shoppingListItems[i].SetIsOnWithoutNotify(true);
            }
        }
        
        // Check passed for all required items
        return check;
    }

    public void OnInteract()
    {
        if (!currentTrader) return;
        
        // TODO: Do other stuff
        Collect(currentTrader.data.itemForSale);
        currentTrader.itemCollected = true;
        currentTrader.E.SetActive(false);
        currentTrader = null;
    }
    
    private static T[] GetAllInstancesInFolder<T>(string folderPath) where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(T).Name, new[] { folderPath }); 
        T[] a = new T[guids.Length];
        for(int i =0; i<guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }
 
        return a;
    }
}
