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

    public ItemObject(string name)
    {
        this.name = name;
        requiredAmount = 0;
        currentAmount = 1;
    }
}
