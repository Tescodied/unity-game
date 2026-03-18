using System.Collections.Generic;
using UnityEngine;

public class CraftingTableLogic : MonoBehaviour
{
    // Mine
    private bool touchingTable = false;
    private List<string> usedScenes = new();

    Player mainScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usedScenes.Add(mainScript.mainSceneName);
    }

    public void MineCheck(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }
        
        if (touchingTable)
        {
            mainScript.enableEnterUI = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                //
            }
        }
    }

    private void EnterTable()
    {
        
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
