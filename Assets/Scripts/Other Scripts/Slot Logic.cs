using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SlotLogic : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private string selfCount;
    public int amount = 0;
    public string materialName;
    public bool mouseUp = false;
    public bool hovered = false;

    private bool showText = false;
    public GameObject imgLocation;
    Vector3 originalPos;
    InventoryManagement parentScript;
    private bool lockToCursor = false;
    public Image img;
    public TextMeshProUGUI amountText;
    ItemDropCrafting dropScript;

    void Awake()
    {
        img = imgLocation.GetComponent<Image>();
        parentScript = GameObject.FindGameObjectWithTag("Inventory Management").GetComponent<InventoryManagement>();

        string name = gameObject.name;
        int start = name.IndexOf('(');
        int end = name.IndexOf(')');

        selfCount = name.Substring(start + 1, end - start - 1);

        materialName = PlayerPrefs.GetString($"Slot {selfCount} (Material)", "");
        amount = PlayerPrefs.GetInt($"Slot {selfCount} (Amount)", 0);
    }

    void Start()
    {
        originalPos = imgLocation.transform.position;
    }

    public void SetSelf(Dictionary<string, Sprite> spriteHashmap)
    {
        foreach (KeyValuePair<string, Sprite> kvp in spriteHashmap)
        {
            if (materialName == kvp.Key)
            {
                ChangeValues(kvp.Value, amount, true);
            }
        }
    }

    public void ChangeValues(Sprite newImg, int newAmount, bool changeAmount)
    {
        if (newImg != null)
        {
            img.sprite = newImg;
        }
        if (changeAmount)
        {
            amount = newAmount;
            amountText.text = amount.ToString();
            PlayerPrefs.SetInt($"Slot {selfCount} (Amount)", amount);
        }

        if (amount == 0)
        {
            img.enabled = false;
            img.sprite = null;
            showText = false;
        }
        else if (amount > 0)
        {
            showText = true;
            img.enabled = true;
        }
        amountText.enabled = showText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (amount > 0)
        {
            lockToCursor = true;
        }
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
    { // Check if user click on the item initially and is holding it
        if (lockToCursor && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            imgLocation.transform.position = mousePos;

            parentScript.childClicked = true;
        } // Stopped holding down
        else if (lockToCursor)
        {
            foreach (GameObject scripterBG in parentScript.craftingBackgrounds)
            {
                ItemDropCrafting scriptBG = scripterBG.GetComponent<ItemDropCrafting>();

                if (scriptBG.hovered && scriptBG.childItemScript.requiredAmount > scriptBG.childItemScript.filledAmount)
                {
                    dropScript = scriptBG;
                    Drop();
                    break;
                }

                foreach (GameObject scripterSlot in parentScript.slots)
                {
                    if (scripterSlot.gameObject == gameObject)
                    {
                        continue;
                    }

                    SlotLogic slotScript = scripterSlot.GetComponent<SlotLogic>();

                    if (slotScript.hovered)
                    {
                        string newSlotMaterialName = slotScript.materialName;
                        int newSlotAmount = slotScript.amount;

                        Debug.Log(newSlotMaterialName + newSlotAmount);

                        // first swap the target slot as its values are saved above 
                        SwapValues(slotScript, materialName, amount);
                        SwapValues(this, newSlotMaterialName, newSlotAmount);
                    }
                }
            }
            lockToCursor = false;
            parentScript.mouseDrop = true;
            imgLocation.transform.position = originalPos;
            // Check if reclick onto a different inventory slot, currently fine 23/03
            parentScript.childClicked = false;
        }
    }

    void Drop()
    {
        amount --;
        dropScript.childItemScript.filledAmount ++;

        dropScript = null;

        ChangeValues(null, amount, true);
    }

    void SwapValues(SlotLogic script, string newMaterial, int newAmount)
    {
        script.ChangeValues(null, newAmount, true);
        script.SetSelf(parentScript.woodScript.nameSprite);
        PlayerPrefs.SetString($"Slot {script.gameObject.name.Split(")")[0].Split("(")[1]} (Material)", newMaterial);
        PlayerPrefs.SetInt($"Slot {script.gameObject.name.Split(")")[0].Split("(")[1]} (Amount)", newAmount);
    }
}
