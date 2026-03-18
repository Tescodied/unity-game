using UnityEngine;
using System.Collections.Generic;

public class ShedBlueprintLogic : MonoBehaviour
{
    // Axe blueprint
    GameObject[] axeBlueprint;
    GameObject shedWorkbenchBlueprint;
    public bool axeBlueprintUnlocked;
    private bool touchingAxeBlueprint = false;
    private List<string> usedScenes = new();

    Player mainScript;

    void Start()
    {
        shedWorkbenchBlueprint = GameObject.FindGameObjectWithTag("Axe Blueprint");
        mainScript = GetComponent<Player>();

        usedScenes.Add(mainScript.shedSceneName);

        if (PlayerPrefs.GetInt("Axe Blueprint Unlocked") == 1)
        {
            if (mainScript.sceneName == mainScript.shedSceneName)
            {
                AxeBlueprintUnlocked();
            }
        }
    }

    public void AxeBlueprintWorkbenchCheck(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }
        
        if (touchingAxeBlueprint && !axeBlueprintUnlocked)
        {
            mainScript.enableEnterUI = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                AxeBlueprintUnlocked();
            }
        }
    }

    private void AxeBlueprintUnlocked()
    {
        axeBlueprintUnlocked = true;

        axeBlueprint = GameObject.FindGameObjectsWithTag("Blueprint Unlock (Axe)");
        foreach (GameObject obj in axeBlueprint)
        {
            Destroy(obj);
        }

        PlayerPrefs.SetInt("Axe Blueprint Unlocked", 1);

        SpriteRenderer workbenchRenderer = shedWorkbenchBlueprint.GetComponent<SpriteRenderer>();
        workbenchRenderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Axe Blueprint")
        {
            touchingAxeBlueprint = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Axe Blueprint")
        {
            touchingAxeBlueprint = false;
        }
    }
}
