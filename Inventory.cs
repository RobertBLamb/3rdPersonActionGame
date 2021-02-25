using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogWarning("more than one inventory!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public List<Item> items = new List<Item>();
    public int inventoryLimit = 20;


    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Add(Item item)
    {
        if(!item.defaultItem)
        {
            if(items.Count >=inventoryLimit)
            {
                Debug.Log("too many items");
                return false;
            }
            items.Add(item);
            if(onItemChangedCallback !=null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
