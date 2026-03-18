using System.Collections.Generic;
using UnityEngine;

public class LeaveColliderLogic : MonoBehaviour
{
    // Leave
    private bool touchingLeave = false;
    private List<string> usedScenes = new();

    Player mainScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usedScenes.Add(mainScript.shedSceneName);
        usedScenes.Add(mainScript.mineSceneName);
    }

    public void LeaveCheck(string leaveScene, string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }

        if (touchingLeave)
        {
            mainScript.enableExitUI = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(leaveScene);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Leave")
        {
            touchingLeave = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Leave")
        {
            touchingLeave = false;
        }
    }
}
