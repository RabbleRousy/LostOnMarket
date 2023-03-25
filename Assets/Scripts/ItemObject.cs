using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class  ItemObject
{
    public string name;
    public int requiredAmount;
    public int currentAmount;

    public ItemObject(string name, int reqAmount, int currAmount)
    {
        this.name = name;
        requiredAmount = reqAmount;
        currentAmount = currAmount;
    }
}
