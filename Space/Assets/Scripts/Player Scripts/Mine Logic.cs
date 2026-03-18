using System.Collections.Generic;
using UnityEngine;

public class MineLogic : MonoBehaviour
{
    // Mine
    private bool touchingMine = false;
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
        
        if (touchingMine)
        {
            mainScript.enableEnterUI = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterMine();
            }
        } // VV change in future to be like mine but wont be due to scene change
        //else
        //{
        //    enterUI.SetActive(false);
        //}
    }

    private void EnterMine()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainScript.mineSceneName);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mine")
        {
            touchingMine = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mine")
        {
            touchingMine = false;
        }
    }
}
