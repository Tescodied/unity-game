using UnityEngine;

public class ToolToggle : MonoBehaviour
{
    Player mainScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void AxeToggle()
    {
        // Only when the player picks up the axe, the bool is set to true
        if (mainScript.axeOnPlayerRendererBack.enabled)
        {
            mainScript.axeOnPlayerBackBool = true;
        }
        else if (mainScript.axeOnPlayerRenderer.enabled)
        {
            mainScript.axeOnPlayerHandBool = true;
        }

        // If User presses 'Q' and actually has the axe
        if (Input.GetKeyDown(KeyCode.Q) && (mainScript.axeOnPlayerBackBool || mainScript.axeOnPlayerHandBool))
        {
            mainScript.axeOnPlayerBackBool = !mainScript.axeOnPlayerBackBool;
            mainScript.axeOnPlayerHandBool = !mainScript.axeOnPlayerHandBool;

            mainScript.axeOnPlayerRenderer.enabled = mainScript.axeOnPlayerHandBool;
            mainScript.axeOnPlayerRendererBack.enabled = mainScript.axeOnPlayerBackBool;
        }
    }

    public void PickaxeToggle()
    {
        // Only when the player picks up the pickaxe, the bool is set to true
        if (mainScript.pickaxeOnPlayerRendererBack.enabled)
        {
            mainScript.pickaxeOnPlayerBackBool = true;
        }
        else if (mainScript.pickaxeOnPlayerRenderer.enabled)
        {
            mainScript.pickaxeOnPlayerHandBool = true;
        }

        // If User presses 'R' and actually has the pickaxe
        if (Input.GetKeyDown(KeyCode.R) && (mainScript.pickaxeOnPlayerBackBool || mainScript.pickaxeOnPlayerHandBool))
        {
            mainScript.pickaxeOnPlayerBackBool = !mainScript.pickaxeOnPlayerBackBool;
            mainScript.pickaxeOnPlayerHandBool = !mainScript.pickaxeOnPlayerHandBool;

            mainScript.pickaxeOnPlayerRenderer.enabled = mainScript.pickaxeOnPlayerHandBool;
            mainScript.pickaxeOnPlayerRendererBack.enabled = mainScript.pickaxeOnPlayerBackBool;
        }
    }
}
