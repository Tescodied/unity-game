using UnityEngine;
using System.Collections.Generic;

public class ShedWorkbenchLogic : MonoBehaviour
{
    // Inside Shed
    private bool touchingShedWorkbench = false;
    private bool firstClickShed = true;
    GameObject axeOnTable;
    GameObject shedWorkbench;
    private List<string> usedScenes = new();

    Player mainScript;

    void Start()
    {
        shedWorkbench = GameObject.FindGameObjectWithTag("Workbench");
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        usedScenes.Add(mainScript.shedSceneName);

        if (PlayerPrefs.GetInt("Equipped Axe") == 1)
        {
            if (mainScript.sceneName == mainScript.shedSceneName)
            {
                EquippedAxeProcedures();
            }
            else
            {
                mainScript.axeOnPlayerRenderer.enabled = true;
            }
        }
    }

    public void ShedWorkbenchCheck(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }
        
        if (touchingShedWorkbench && firstClickShed)
        {
            mainScript.enableEnterUI = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                EquippedAxeProcedures();
            }
        }
    }

    private void EquippedAxeProcedures()
    {
        firstClickShed = false;

        axeOnTable = GameObject.Find("Workbench Axe Decoration (Player Equip)");
        Destroy(axeOnTable);

        mainScript.axeOnPlayerRenderer.enabled = true;
        mainScript.axeOnPlayerHandBool = true;
        PlayerPrefs.SetInt("Equipped Axe", 1);

        SpriteRenderer workbenchRenderer = shedWorkbench.GetComponent<SpriteRenderer>();
        workbenchRenderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Workbench")
        {
            touchingShedWorkbench = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Workbench")
        {
            touchingShedWorkbench = false;
        }
    }
}