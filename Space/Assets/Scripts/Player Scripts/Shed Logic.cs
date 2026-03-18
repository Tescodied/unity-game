using UnityEngine;
using System.Collections.Generic;

public class ShedLogic : MonoBehaviour
{
    // Shed
    private bool touchingShed = false;
    private List<string> usedScenes = new();

    Player mainScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usedScenes.Add(mainScript.mainSceneName);
    }

    public void ShedCheck(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }
        
        if (touchingShed)
        {
            mainScript.enableEnterUI = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Shed");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shed")
        {
            touchingShed = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shed")
        {
            touchingShed = false;
        }
    }
}
