using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingLogic : MonoBehaviour
{
    // Fishing State
    private bool touchingFishing = false;
    private bool fishMode = false;
    private List<string> usedScenes = new();

    // References
    public Transform fishingPositioner;
    private Vector3 fishingPosition;
    public List<GameObject> fishingGameObjects = new();
    public SpriteRenderer fishingRod;

    private bool sceneIsMain;
    Player mainScript;
    MovementLogic movementScript;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        movementScript = GetComponent<MovementLogic>();
        sceneIsMain = SceneManager.GetActiveScene().name == mainScript.mainSceneName;

        usedScenes.Add(mainScript.mainSceneName);

        if (fishingPositioner != null)
        {
            fishingPosition = fishingPositioner.position;
        }
    }

    public void FishingCheck(string sceneName)
    {
        if (!sceneIsMain || fishingRod == null || !usedScenes.Contains(sceneName))
            return;

        if (touchingFishing)
        {
            SetRodAlpha(0.4f);

            if (!fishMode)
            {
                mainScript.enableEnterUI = true;
            }
            else
            {
                mainScript.enableExitUI = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                fishMode = !fishMode;

                if (fishMode)
                {
                    EnterFishingMode();
                }
                else
                {
                    ExitFishingMode();
                }
            }
        }
        else
        {
            SetRodAlpha(1f);
        }
    }

    private void EnterFishingMode()
    {
        // State
        mainScript.isFishing = true;
        mainScript.disableMovement = true;

        // Camera
        mainScript.ChangeCamSize(mainScript.enlargenedLens);

        // Movement reset + reposition
        movementScript.ResetMovement(movementScript.rigidBody);
        mainScript.transform.position = fishingPosition;

        // UI toggle
        foreach (GameObject UI in fishingGameObjects)
        {
            UI.SetActive(!UI.activeSelf);
        }

        SetRodAlpha(0.2f);
        mainScript.animator.enabled = false;
    }

    private void ExitFishingMode()
    {
        // State
        mainScript.isFishing = false;
        mainScript.disableMovement = false;

        // Camera reset
        mainScript.ChangeCamSize(mainScript.defaultLens);

        // UI toggle back
        foreach (GameObject UI in fishingGameObjects)
        {
            UI.SetActive(!UI.activeSelf);
        }

        // Animation back on
        mainScript.animator.enabled = true;
    }

    private void SetRodAlpha(float alpha)
    {
        Color color = fishingRod.color;
        color.a = alpha;
        fishingRod.color = color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fishing"))
        {
            touchingFishing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fishing"))
        {
            touchingFishing = false;

            // Force exit if player walks away mid-fishing
            if (fishMode)
            {
                fishMode = false;
                ExitFishingMode();
            }
        }
    }
}