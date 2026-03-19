using System.Collections.Generic;
using UnityEngine;

public class RecipeUnlockCheck : MonoBehaviour
{
    List<GameObject> recipes = new List<GameObject>();
    public string recipePlayerPrefPreffix = "Recipe: ";
    List<string> recipesMade = new List<string>();
    public Dictionary<string, int> playerRecipesUnlocked = new Dictionary<string, int>();

    void Awake()
    {
        recipesMade.Add("Wooden Axe");
        recipesMade.Add("Wooden Pickaxe");

        foreach (string recipe in recipesMade)
        { // "Recipe: Wooden Axe"
            playerRecipesUnlocked[recipe] = PlayerPrefs.GetInt(recipePlayerPrefPreffix + recipe);    
            recipes.Add(GameObject.FindGameObjectWithTag($"Crafting Recipe ({recipe})"));
        }
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
            // Debug.Log($"Crafting Recipe Locked ({recipeNameSuffix})    {overlay}");
            overlay.SetActive(playerRecipesUnlocked[recipeNameSuffix] != 1);
        }

        GameObject craftingUI;
        craftingUI = GameObject.FindGameObjectWithTag("Crafting UI");
        craftingUI.SetActive(false);
    }
}
