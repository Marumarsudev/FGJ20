﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class InventoryItem : ScriptableObject
{

    public string itemName;
    public Sprite itemImage;
    public string itemDescription;
}