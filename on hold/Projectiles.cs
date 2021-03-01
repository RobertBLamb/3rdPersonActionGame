using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Ammo")]
public class Projectiles : Item
{
    public LayerMask canHit;
    //public int ammoCount;
}
