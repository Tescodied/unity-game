using System.Collections.Generic;
using UnityEngine;

public class TreeWoodGain : MonoBehaviour
{
    Player mainScript;
    List<string> treeTypes = new List<string>();
    List<string> regularTreeLog = new List<string>();
    List<string> regularTreeStick = new List<string>();
    int sticks;
    int longLogs;
    int shortLogs;

    private List<string> usedScenes = new();

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usedScenes.Add(mainScript.mainSceneName);
    }

    void Awake()
    {
        // All tree types, add more in future
        treeTypes.Add("Stump");
        treeTypes.Add("Short");
        treeTypes.Add("Tall");

        // Possible Log lengths and count when break a tree
        regularTreeLog.Add("LongLog:1, Short Log:1");
        regularTreeLog.Add("LongLog:1");
        regularTreeLog.Add("ShortLog:2");
        regularTreeLog.Add("ShortLog:3");

        // Possible number of sticks when break a tree
        regularTreeStick.Add("Stick:1");
        regularTreeStick.Add("Stick:2");
        regularTreeStick.Add("Stick:3");
        regularTreeStick.Add("Stick:4");

        sticks = PlayerPrefs.GetInt("Sticks");
        longLogs = PlayerPrefs.GetInt("Long Logs");
        shortLogs = PlayerPrefs.GetInt("Short Logs");
    }

    private string generateWood(string treeType)
    {
        string logs = "";
        string sticks = "";

        if (treeType == "Short")
        {
            System.Random random = new System.Random();
            int sitckIndex = random.Next(regularTreeStick.Count);
            int logIndex = random.Next(regularTreeLog.Count);

            sticks = regularTreeStick[sitckIndex];
            logs = regularTreeLog[logIndex];
        }

        string resultingItems = sticks + "," + logs;

        return resultingItems;
    } // num sticks (int), log type (str) & num logs

    public void AddWood(string treeType)
    { // set different wood types and values and materials

        string items = generateWood(treeType);
        string[] itemsSeperated = items.Split(',');

        foreach (string item in itemsSeperated)
        {
            string[] nameCount = item.Split(":");
            string name = nameCount[0];
            int count = int.Parse(nameCount[1]);

            if (name == "LongLog")
            {
                longLogs += count;
            }
            else if (name == "ShortLog")
            {
                shortLogs += count;
            }
            else if (name == "Stick")
            {
                sticks += count;
            }
        }

        PlayerPrefs.SetInt("Sticks", sticks);
        PlayerPrefs.SetInt("Long Logs", longLogs);
        PlayerPrefs.SetInt("Short Logs", shortLogs);
    }

    public void InventoryUpdate(string sceneName)
    {
        if (!usedScenes.Contains(sceneName))
        {
            return;
        }

        mainScript.inventory["Sticks"] = sticks;
        mainScript.inventory["Long Logs"] = longLogs;
        mainScript.inventory["Short Logs"] = shortLogs;
    }
}
