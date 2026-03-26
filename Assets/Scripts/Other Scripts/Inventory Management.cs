using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject[] disableOverlays;
    public GameObject[] craftingBackgrounds; // use finding alogrithm once multiple crafting recipes
    public GameObject craftingControl;
    CraftingAppearanceUI craftingScript;
    public TreeWoodGain woodScript;
    public bool childClicked = false;
    public bool mouseDrop = false;

    void Awake()
    {
        slots = GameObject.FindGameObjectsWithTag("Inventory Slot");
        woodScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TreeWoodGain>();
        craftingScript = craftingControl.GetComponent<CraftingAppearanceUI>();
        craftingBackgrounds = GameObject.FindGameObjectsWithTag("Container Background Configure");
        
        Array.Reverse(slots);

        foreach (GameObject slot in slots)
        {
            SlotLogic slotScript = slot.GetComponent<SlotLogic>();
            
            slotScript.SetSelf(woodScript.nameSprite);
        }
    }

    public void AddItem(string itemName, int amount, Sprite img)
    {
        foreach (GameObject slot in slots)
        {
            SlotLogic slotScript = slot.GetComponent<SlotLogic>();

            if (slotScript.amount == 0)
            {
                // Change the slot values to new values
                PlayerPrefs.SetString($"Slot {Array.IndexOf(slots, slot) + 1} (Material)", itemName);
                slotScript.ChangeValues(img, amount, true);

                //PlayerPrefs.SetString(slot.name, $"{itemName}: {amount}");

                break;
            }
        }
    }

    void DisableOverlay(bool enable)
    {
        foreach (GameObject overlay in disableOverlays)
        {
            string name = overlay.name;
            int start = name.IndexOf('(');
            int end = name.IndexOf(')');

            string nameCategory = name.Substring(start + 1, end - start - 1);
            if (nameCategory == "Crafting" && craftingScript.recipeActive)
            {
                overlay.SetActive(false);
                return;
            }
            overlay.SetActive(enable);
        }
    }

    void ConfigureCraftingBackgrounds()
    {
        foreach (GameObject bg in craftingBackgrounds)
        {
            Image img = bg.GetComponent<Image>();
            img.enabled = true;
        }
    }

    void Update()
    {
        DisableOverlay(childClicked);
    }
}
