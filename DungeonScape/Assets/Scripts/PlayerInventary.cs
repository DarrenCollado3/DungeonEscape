using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private HashSet<string> obtainedItems = new HashSet<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ObtainItem(string itemName)
    {
        obtainedItems.Add(itemName);
    }

    public bool HasItem(string itemName)
    {
        return obtainedItems.Contains(itemName);
    }

    public void ClearSpecialItem()
    {
        obtainedItems.Remove("SpecialItem");
    }
}
