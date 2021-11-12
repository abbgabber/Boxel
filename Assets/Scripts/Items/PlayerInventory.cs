using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            if (inventory.AddItem(new Item(item.item), 1))
            {
                Destroy(other.gameObject);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
            Debug.Log("Save");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.Load();
            equipment.Load();
            Debug.Log("Load");
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
}