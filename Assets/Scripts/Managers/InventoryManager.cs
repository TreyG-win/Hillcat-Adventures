using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script holds the methods that are needed to
 * handle the player's inventory.
 * 
 * This script should be attached to an empty game object
 * to act as a manager (preferrably the same game object as
 * Managers.cs or where ever the managers are being kept).
 */
public class InventoryManager : MonoBehaviour, IGameManagers
{
    //Property can be read from anywhere but set only within this script
    public ManagerStatus status { get; private set; }

    public string equippedItem { get; private set; }

    private Dictionary<string, int> items;

    //Initializes the inventory manager, similar to a Start() method.
    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        items = new Dictionary<string, int>();

        status = ManagerStatus.Started;
    }
    //Allows for all of the items in the player's inventory to be displayed.
    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in items)
        {
            itemDisplay += item + " ";
        }
        Debug.Log(itemDisplay);
    }
    //Allows the player to add a new item to their inventory.
    //This can be seen in use in CollectibleItem.cs
    public void AddItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {

            items[name] = 1;
        }

        DisplayItems();
    }

    //Returns the list of all items in the player's inventory
    //This is used in a few different scripts, such as DeviceTrigger.cs
    public List<string> GetItemList()
    {
        List<string> list = new List<string>(items.Keys);
        return list;
    }

    //Returns how much of a particular item the player has.
    //Currently unused, but can allow for multiple of the same item to
    //be collected.
    public int GetItemCount(string name)
    {
        if (items.ContainsKey(name))
        {
            return items[name];
        }
        return 0;

    }

    //Allows the player to equip an item
    public bool EquipItem(string name)
    {
        if (items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            Debug.Log($"Equipped {name}");
            return true;
        }

        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    //Consumes an item upon being used (useful for temporary items)
    public bool ConsumeItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name]--;
            if (items[name] == 0)
            {
                items.Remove(name);
            }
        }
        else
        {
            Debug.Log($"Cannot consume {name}");
            return false;
        }
        DisplayItems();
        return true;
    }
}
