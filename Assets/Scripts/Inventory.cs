using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemObject> currentInventory;
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask traderMask;

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
        Collider2D traderCollider = Physics2D.OverlapCircle(transform.position,interactionRange,traderMask);
        if (traderCollider)
        {
            // TODO: Do other stuff
            
            
            Collect(traderCollider.GetComponent<Trader>().data.itemForSale);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
