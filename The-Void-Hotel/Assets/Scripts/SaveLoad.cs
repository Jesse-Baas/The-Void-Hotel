using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveTest : MonoBehaviour
{
    private string savePath;
    public Transform player;
    public Timer timerScript;
    public InventoryManager inventory; 

    void Start()
    {
        savePath = Application.persistentDataPath + "/gameSave.json";
    }

    public void SaveGame()
    {
        List<int> inventoryIndices = new List<int>();
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject item in inventory.inventory)
        {
            int index = System.Array.IndexOf(allItems, item); // Find index in world
            if (index != -1)
            {
                inventoryIndices.Add(index);
            }
        }

        SaveData saveData = new SaveData(
            player.position,
            timerScript.GetTimer(),
            timerScript.IsTimerRunning(),
            inventoryIndices
        );

        string jsonData = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

            player.position = data.position;
            timerScript.SetTimer(data.timer);
            timerScript.SetTimerRunning(data.isTimerRunning);

            inventory.LoadInventory(data.inventoryIndices);
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save file found!");
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public Vector3 position;
        public float timer;
        public bool isTimerRunning;
        public List<int> inventoryIndices; // New: Save inventory item indices

        public SaveData(Vector3 playerPosition, float gameTimer, bool timerRunning, List<int> inventory)
        {
            position = playerPosition;
            timer = gameTimer;
            isTimerRunning = timerRunning;
            inventoryIndices = inventory; // Store inventory item indices
        }
    }
}
