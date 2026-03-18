using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    // General Unity Components and Object
    public Animator animator;

    public string mainSceneName = "Main";
    public string shedSceneName = "Shed";
    public string mineSceneName = "Mine";
    public string sceneName;
    private bool sceneIsMain;
    public List<string> scenesLargeCam = new List<string> { "Shed" };
    // When the player spawns, it will be at the bottom of the shed but this makes sure the camera is centred
    private Vector2 shedCamOffset = new(0, 2.25f);

    // UI bools
    public bool enableEnterUI;
    public bool enableExitUI;

    // CineMachine
    public float defaultLens;
    public float enlargenedLens;
    public float enlargenedLensMultiplier = 0.5f;
    public float largeCamSize = 4.5f;
    CinemachineCamera vcam;

    // Movement
    public bool disableMovement = false;
    
    // Axe
    GameObject axeOnPlayer;
    GameObject axeOnPlayerBack;
    public SpriteRenderer axeOnPlayerRenderer;
    public SpriteRenderer axeOnPlayerRendererBack;
    public bool axeOnPlayerHandBool = false;
    public bool axeOnPlayerBackBool = false;
    
    // Pickaxe
    GameObject pickaxeOnPlayer;
    GameObject pickaxeOnPlayerBack;
    public  SpriteRenderer pickaxeOnPlayerRenderer;
    public SpriteRenderer pickaxeOnPlayerRendererBack;
    public bool pickaxeOnPlayerHandBool = false;
    public bool pickaxeOnPlayerBackBool = false;

    // Wood
    public static int wood = 0;
    public TextMeshProUGUI woodCount;

    // Fishing (Overrider)
    public bool isFishing = false;

    //Scripts
    FishingLogic fishingScript;
    ShedLogic shedScript;
    ShedWorkbenchLogic shedWorkbenchScript;
    ShedBlueprintLogic axeBlueprintScript;
    MineLogic mineScript;
    MineWorkbenchLogic mineWorkbenchLogic;
    ToolToggle toolToggleScript;
    LeaveColliderLogic leaveColliderScript;
    ConfigureUILogic uiScript;
    MovementLogic movementScript;

    void Awake()
    {
        // Scene Configuration
        sceneName = SceneManager.GetActiveScene().name;
        sceneIsMain = sceneName == mainSceneName;

        // Player Axe
        axeOnPlayer = GameObject.FindGameObjectWithTag("Player Axe");
        axeOnPlayerRenderer = axeOnPlayer.GetComponent<SpriteRenderer>();
        axeOnPlayerBack = GameObject.FindGameObjectWithTag("Player Axe (On Back)");
        axeOnPlayerRendererBack = axeOnPlayerBack.GetComponent<SpriteRenderer>();

        // Player Pickaxe
        pickaxeOnPlayer = GameObject.FindGameObjectWithTag("Player Pickaxe");
        pickaxeOnPlayerRenderer = pickaxeOnPlayer.GetComponent<SpriteRenderer>();
        pickaxeOnPlayerBack = GameObject.FindGameObjectWithTag("Player Pickaxe (On Back)");
        pickaxeOnPlayerRendererBack = pickaxeOnPlayerBack.GetComponent<SpriteRenderer>();

        // Camera
        vcam = FindObjectOfType<CinemachineCamera>();
        float fov = vcam.Lens.OrthographicSize;
        defaultLens = fov;
        enlargenedLens = fov * enlargenedLensMultiplier;


        if (scenesLargeCam.Contains(sceneName))
        {
            ChangeCamSize(largeCamSize);
            vcam.Follow = null;
            vcam.transform.position = new Vector3(vcam.transform.position.x + shedCamOffset.x, vcam.transform.position.y + shedCamOffset.y, vcam.transform.position.z);
        }
        
        animator = GetComponent<Animator>();

        // Scripts
        fishingScript = GetComponent<FishingLogic>();
        shedScript = GetComponent<ShedLogic>();
        shedWorkbenchScript = GetComponent<ShedWorkbenchLogic>();
        axeBlueprintScript = GetComponent<ShedBlueprintLogic>();
        mineScript = GetComponent<MineLogic>();
        mineWorkbenchLogic = GetComponent<MineWorkbenchLogic>();
        toolToggleScript = GetComponent<ToolToggle>();
        leaveColliderScript = GetComponent<LeaveColliderLogic>();
        uiScript = GetComponent<ConfigureUILogic>();
        movementScript = GetComponent<MovementLogic>();

        // Test mode:
        //PlayerPrefs.DeleteKey("Equipped Axe");
        //PlayerPrefs.DeleteKey("Equipped Pickaxe");
    }

    void Update()
    {
        // Reset at the start of frame only on if triggered
        enableEnterUI = false;
        enableExitUI = false;

        // Move if not locked/running into an object
        if (!disableMovement && !isFishing)
        {
            movementScript.Movement();
        }

        // Collision Checks
        shedScript.ShedCheck(sceneName);
        shedWorkbenchScript.ShedWorkbenchCheck(sceneName);
        axeBlueprintScript.AxeBlueprintWorkbenchCheck(sceneName);
        mineScript.MineCheck(sceneName);
        mineWorkbenchLogic.MineWorkbenchCheck(sceneName);
        leaveColliderScript.LeaveCheck(mainSceneName, sceneName);
        fishingScript.FishingCheck(sceneName);

        // Player UI enable/disable
        uiScript.ConfigureUI();

        // Wood
        woodCount.text = $"{wood}";

        // Tools
        toolToggleScript.AxeToggle();
        toolToggleScript.PickaxeToggle();
    }

    public void ChangeCamSize(float size)
    {
        var lens = vcam.Lens;
        lens.OrthographicSize = size;
        vcam.Lens = lens;
    }

    public void AddWood()
    { // set different wood types and values and materials
        wood += 1;
    }
}