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
        enterUI.SetActive(mainScript.enableEnterUI);

        // Exit UI
        exitUI.SetActive(mainScript.enableExitUI);
    }
}
