using UnityEngine;
using System.Collections.Generic;

public class MineWorkbenchLogic : MonoBehaviour
{
    private bool touchingMineWorkbench = false;
    private bool firstClickMine = true;
    GameObject mineWorkbench;
    GameObject pickaxeOnTable;
    private List<string> usedScenes = new();

    Player mainScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mineWorkbench = GameObject.FindGameObjectWithTag("Pickaxe Workbench");

        usedScenes.Add(mainScript.mineSceneName);

        if (PlayerPrefs.GetInt("Equipped Pickaxe") == 1)
        {
            if (mainScript.sceneName == mainScript.mineSceneName)
            {
                EquippedPickaxeProcedures(true);
            }
        }
    }

    public void MineWorkbenchCheck(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }
        
        if (touchingMineWorkbench && firstClickMine)
        {
            mainScript.enableEnterUI = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                EquippedPickaxeProcedures(false);
            }
        }
    }

    private void EquippedPickaxeProcedures(bool start)
    {
        firstClickMine = false;

        pickaxeOnTable = GameObject.Find("Workbench Pickaxe Decoration (Player Equip)");
        Destroy(pickaxeOnTable);

        mainScript.pickaxeOnPlayerRenderer.enabled = start;
        mainScript.pickaxeOnPlayerHandBool = start;
        PlayerPrefs.SetInt("Equipped Pickaxe", 1);

        SpriteRenderer workbenchRenderer = mineWorkbench.GetComponent<SpriteRenderer>();
        workbenchRenderer.enabled = false;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickaxe Workbench")
        {
            touchingMineWorkbench = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickaxe Workbench")
        {
            touchingMineWorkbench = false;
        }
    }
}
