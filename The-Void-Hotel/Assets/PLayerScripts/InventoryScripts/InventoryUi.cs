using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUi : MonoBehaviour
{
    public GameObject inventoryPanel;
    public bool isInventoryOpen = false;

    public Button[] inventoryButtons;
    public InventoryManager inventorySystem;
    public TextMeshProUGUI selectedSlotDisplay;


    void Start()
    {
        if (inventoryButtons.Length == 0 || selectedSlotDisplay == null || inventorySystem == null)
        {
            Debug.LogError("No buttons or slots assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
            UpdateButtonColors();

            if (isInventoryOpen)
            {
                UpdateSelectedItemText();
            }
        }
        if (isInventoryOpen)
        {
            UpdateButtonColors();
        }
    }

    void UpdateSelectedItemText()
    {
        int selectedItemIndex = inventorySystem.selectedItemIndex;

        if (selectedItemIndex >= 0 && selectedItemIndex < inventorySystem.inventory.Count)
        {
            selectedSlotDisplay.text = $"Selected item: {inventoryButtons[selectedItemIndex].name}";
        }
        else
        {
            selectedSlotDisplay.text = "No item selected";
        }
    }

    void UpdateButtonColors()
    {
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            Button button = inventoryButtons[i];

            if (i < inventorySystem.inventory.Count && inventorySystem.inventory[i] != null)
            {
                button.GetComponent<Image>().color = Color.green;
            }
            else
            {
                button.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
