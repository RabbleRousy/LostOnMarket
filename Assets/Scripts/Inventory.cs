using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemObject> currentInventory;

    public Trader currentTrader = null;

    public void Collect(string itemName)
    {
        int index = currentInventory.FindIndex(item => item.name == itemName);
        if (index != -1)
        {
            currentInventory[index].currentAmount += 1;
        }
        else
        {
            currentInventory.Add(new ItemObject(itemName));
        }
    }

    public bool CheckShoppingList()
    {
        foreach (var item in currentInventory)
        {
            // Item is not required, skip check
            if (item.requiredAmount == 0) continue;

            if (item.currentAmount < item.requiredAmount) return false;
        }
        
        // Check passed for all required items
        return true;
    }

    public void OnInteract()
    {
        if (!currentTrader) return;
        
        // TODO: Do other stuff
        Collect(currentTrader.data.itemForSale);
    }
}
