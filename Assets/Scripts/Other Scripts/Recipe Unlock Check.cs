using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUnlockCheck : MonoBehaviour
{
    List<GameObject> recipes = new();
    public GameObject craftingControl;
    CraftingAppearanceUI craftingScript;
    public string recipePlayerPrefPreffix = "Recipe: ";
    List<string> recipesMade = new();
    public Dictionary<string, int> playerRecipesUnlocked = new Dictionary<string, int>();
    List<RecipeCheckClicked> recipeScripts = new();
    public List<GameObject> CraftingUILayouts;

    public GameObject[] craftingImgs; // for debug, can be assigned locally
    public GameObject craftingProgressBar;

    void Awake()
    {
        recipesMade.Add("Wooden Axe");
        recipesMade.Add("Wooden Pickaxe");

        PlayerPrefs.SetInt("Recipe: Wooden Pickaxe", 1);

        foreach (string recipe in recipesMade)
        { // "Recipe: Wooden Axe"
            playerRecipesUnlocked[recipe] = PlayerPrefs.GetInt(recipePlayerPrefPreffix + recipe);
            recipes.Add(GameObject.FindGameObjectWithTag($"Crafting Recipe ({recipe})"));
        }

        craftingScript = craftingControl.GetComponent<CraftingAppearanceUI>();
    }

    void Start()
    {
        foreach (GameObject recipe in recipes)
        {
            string name = recipe.name;
            int start = name.IndexOf('(');
            int end = name.IndexOf(')');

            string recipeNameSuffix = name.Substring(start + 1, end - start - 1);
            GameObject overlay = GameObject.FindGameObjectWithTag($"Crafting Recipe Locked ({recipeNameSuffix})");

            overlay.SetActive(playerRecipesUnlocked[recipeNameSuffix] != 1);
            recipeScripts.Add(recipe.GetComponent<RecipeCheckClicked>());
        }

        // After usage/initialisation, turn off craftingUI display
        GameObject craftingUI;
        craftingUI = GameObject.FindGameObjectWithTag("Crafting UI");
        craftingUI.SetActive(false);
    }

    public void UpdateChildren(RecipeCheckClicked script)
    {
        // Change Recipe Section
        for (int i = 0; i < recipeScripts.Count; i++)
        {
            if (recipeScripts[i].clicked && !(recipeScripts[i] == script))
            {
                recipeScripts[i].clicked = !recipeScripts[i].clicked;
                break;
            }
        }

        craftingScript.ToggleUIActivity(true);

        script.clicked = !script.clicked;

        // Change Crafting Section Fix in future for lag issue
        foreach (GameObject layout in CraftingUILayouts)
        {
            if (!script.clicked)
            {
                layout.SetActive(false);
                craftingProgressBar.SetActive(false);
                continue;
            }

            string name = layout.name;
            int start = name.IndexOf('(');
            int end = name.IndexOf(')');

            string layoutSuffix = name.Substring(start + 1, end - start - 1);
            if (layoutSuffix == script.layout)
            {
                layout.SetActive(true);
                craftingProgressBar.SetActive(true);

                craftingImgs = GameObject.FindGameObjectsWithTag("Crafting Inactive");

                foreach (GameObject img in craftingImgs)
                {
                    Image imgImg = img.GetComponent<Image>();

                    string[] split = img.name.Split(" ");
                    // Item/Result (1/2)
                    string imgName = split[0];
                    string count = split[1];

                    GameObject[] locator = null;
                    if (imgName == "Item")
                    {
                        locator = script.itemsNecessary;
                    }
                    else if (imgName == "Result")
                    {
                        locator = script.itemsResult;
                    }

                    if (locator == null) continue;

                    foreach (GameObject item in locator)
                    {
                        string[] splitStr = item.name.Split(" ");
                        string orderIndex = splitStr[splitStr.Length - 1];

                        if (orderIndex == count)
                        {
                            Image itemImg = item.GetComponent<Image>();
                            imgImg.sprite = itemImg.sprite;
                        }
                    }
                }
            }
        }
    }
}