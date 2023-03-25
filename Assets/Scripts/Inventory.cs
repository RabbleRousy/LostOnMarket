using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemObject> currentInventory;

    public Trader currentTrader = null;

    [SerializeField] private Toggle[] shoppingListItems;

    private void Start()
    {
        SetUpShoppingList();
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
            currentInventory.Add(new ItemObject(itemName));
        }

        CheckShoppingList();
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
        return true;
    }

    public void OnInteract()
    {
        if (!currentTrader) return;
        
        // TODO: Do other stuff
        Collect(currentTrader.data.itemForSale);
    }
}
