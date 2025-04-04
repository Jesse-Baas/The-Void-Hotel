using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Settings")]
    public int maxInventorySize;
    public float pickupRange;
    public float dropRange;

    [Header("Inventory")]
    public List<GameObject> inventory = new List<GameObject>();
    public int selectedItemIndex = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpItem();
        }

        // Number keys for selection (1-9, 0)
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + (i == 9 ? 0 : i + 1)))
            {
                SelectItem(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropSelectedItem();
        }
    }

    private void TryPickUpItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (inventory.Count < maxInventorySize)
                {
                    GameObject item = hit.collider.gameObject;
                    inventory.Add(item);
                    item.SetActive(false); // Hide item from world
                    Debug.Log($"Picked up {item.name}");
                }
                else
                {
                    Debug.Log("Inventory full!");
                }
            }
        }
    }

    private void SelectItem(int index)
    {
        if (index >= 0 && index < inventory.Count)
        {
            selectedItemIndex = index;
            Debug.Log($"Selected item: {inventory[selectedItemIndex].name}");
        }
        else
        {
            Debug.Log("Invalid selection or empty slot.");
        }
    }

    private void DropSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < inventory.Count)
        {
            GameObject item = inventory[selectedItemIndex];
            inventory.RemoveAt(selectedItemIndex);

            item.transform.position = transform.position + transform.forward * dropRange;
            item.SetActive(true); // Show item in world

            Debug.Log($"Dropped {item.name}");

            selectedItemIndex = -1;
        }
        else
        {
            Debug.Log("No item selected to drop.");
        }
    }
    public void LoadInventory(List<int> savedIndices)
    {
        inventory.Clear(); // Clear old inventory

        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Item");

        foreach (int index in savedIndices)
        {
            if (index >= 0 && index < allItems.Length)
            {
                GameObject item = allItems[index];
                inventory.Add(item);
                item.SetActive(false); // Hide item since it's in inventory
            }
        }
    }
}
