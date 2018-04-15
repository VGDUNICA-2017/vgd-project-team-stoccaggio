using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController {

    // info del personaggio
    public Equipment playerInventory = new Equipment();
}

public class Item
{
    private string name;
    public string Name { get { return name; } }

    private string displayName;
    public string DisplayName { get { return displayName; } }

    public Item(string name, string displayName)
    {
        this.name = name;
        this.displayName = displayName;
    }
}

public class Equipment
{
    private int currentIndex;
    private List<Item> itemList;

    public Equipment()
    {
        currentIndex = 0;
        itemList = new List<Item>();
    }

    public bool IsEquipped(Item item)
    {
        bool flag = false;

        if(itemList.Contains(item))
        {
            flag = true;
        }
        
        return flag;
    }

    public List<Item> GetItems()
    {
        return itemList;
    }

    public Item CurrentItem()
    {
        return itemList[currentIndex];
    }

    public void EquipNext()
    {
        currentIndex = (currentIndex + 1) % itemList.Count;
    }

    public void EquipPrevious()
    {
        currentIndex = (currentIndex == 0) ? itemList.Count - 1 : currentIndex - 1;
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(string name)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].Name == name)
            {
                itemList.RemoveAt(i);
                if (i <= currentIndex)
                    EquipPrevious();
                return;
            }
        }
    }

    public List<Item> GetOrderedList()
    {
        int k = currentIndex;
        List<Item> orderedList = new List<Item>();

        for(int i = 0; i < itemList.Count; i++)
        {
            orderedList.Add(itemList[k]);
            k = (k + 1) % itemList.Count;
        }

        return orderedList;
    }
}