using System.Collections.Generic;
using UnityEngine;

public class CraftingTableLogic : MonoBehaviour
{
    // Table
    private bool touchingTable = false;
    private bool craftingMode = false;
    public bool clickOff = false;
    private List<string> usedScenes = new();
    
    // GO refs
    public GameObject overlay;
    public GameObject craftingUI;

    // Scripts
    Player mainScript;
    MovementLogic movementScript;

    void Start()
    {
        mainScript = GetComponent<Player>();
        movementScript = GetComponent<MovementLogic>();
        usedScenes.Add(mainScript.mainSceneName);
    }

    public void CraftingCheck(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }
        
        if (touchingTable)
        {
            mainScript.enableEnterUI = !craftingMode;

            if (Input.GetKeyDown(KeyCode.E) || clickOff)
            {
                craftingMode = !craftingMode;
                clickOff = false;
                if (craftingMode)
                {
                    EnterTableMode();
                }
                else
                {
                    ExitTableMode();
                }
            }
        }
    }

    public void EnterTableMode()
    {
        mainScript.isCrafting = true;
        mainScript.disableMovement = true;

        movementScript.ResetMovement(movementScript.rigidBody);
        mainScript.craftMenuEnable = true;

        overlay.SetActive(true);
        craftingUI.SetActive(true);
    }

    public void ExitTableMode()
    {
        mainScript.isCrafting = false;
        mainScript.disableMovement = false;

        mainScript.craftMenuEnable = false;

        overlay.SetActive(false);
        craftingUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crafting Table")
        {
            touchingTable = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crafting Table")
        {
            touchingTable = false;
        }
    }
}
