using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropCrafting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryManagement inventoryScript;
    public GameObject childItem;
    public CraftingItemTracker childItemScript;
    public bool hovered;
    public GameObject checkbox;
    CheckboxCraftingUI checkboxScript;

    void Awake()
    {
        childItemScript = childItem.GetComponent<CraftingItemTracker>();
        inventoryScript = GameObject.FindGameObjectWithTag("Inventory Management").GetComponent<InventoryManagement>();
        checkboxScript = checkbox.GetComponent<CheckboxCraftingUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

    void Update()
    {
        if (childItemScript.CheckFilled())
        {
            checkboxScript.CheckTickbox();
        } else
        {
            checkboxScript.UncheckTickbox();
        }
    }
}
