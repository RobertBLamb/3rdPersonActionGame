using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool defaultItem = false;

    public virtual void Use()
    {

    }
}
