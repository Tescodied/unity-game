using UnityEngine;

public class ConfigureUILogic : MonoBehaviour
{
    // UI
    public GameObject enterUI;
    public GameObject exitUI;

    Player mainScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void ConfigureUI()
    {
        // Enter UI
        if (mainScript.enableEnterUI)
        {
            enterUI.SetActive(true);
        }
        else
        {
            enterUI.SetActive(false);
        }

        // Exit UI
        if (mainScript.enableExitUI)
        {
            exitUI.SetActive(true);
        }
        else
        {
            exitUI.SetActive(false);
        }
    }
}
