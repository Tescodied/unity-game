using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingAppearanceUI : MonoBehaviour
{
    public bool recipeActive = false;
    GameObject defaultUI;
    GameObject[] alphaControlledGO;
    List<Image> alphaControlledUI = new();
    private const float inactiveAlpha = 25f;
    private float alpha = inactiveAlpha / 255f;

    void Awake()
    {
        defaultUI = GameObject.FindGameObjectWithTag("Default Crafting UI");
        alphaControlledGO = GameObject.FindGameObjectsWithTag("Crafting Inactive");
    }
    
    void Start()
    {
        foreach (GameObject UI in alphaControlledGO)
        {
            Image image = UI.GetComponent<Image>();
            alphaControlledUI.Add(image);
        }

        ToggleUIActivity(false);
    }

    public void ToggleUIActivity(bool enable)
    {
        foreach (Image UI in alphaControlledUI)
        {
            Color col = UI.color;
            col.a = enable ? 1f : alpha;
            UI.color = col;
        }
        recipeActive = enable;
    }
}
