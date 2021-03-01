using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;

    public float pickupRadius = 3f;

    public Transform player;

    bool pickedUp;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance<=pickupRadius && Input.GetButtonDown("Interact"))
        {
            Debug.Log("touching");
            pickedUp = true;
            Pickup();
            //Interact();
        }
    }

    public virtual void Interact()
    {
        //this method will be overwritten by other scripts

    }

    public void Pickup()
    {
        bool added = Inventory.instance.Add(item);
        if(added)
        {
            Destroy(gameObject);
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
