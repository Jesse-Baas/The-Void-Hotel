using UnityEngine;
using System.IO;

public class SaveTest : MonoBehaviour
{
    private string savePath;
    public Transform player;
    public Timer timerScript;

    void Start()
    {
        savePath = Application.persistentDataPath + "/gameSave.json";
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData(
            player.position,
            timerScript.GetTimer(),
            timerScript.IsTimerRunning()
        );

        string jsonData = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, jsonData);
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

        }
    }

    [System.Serializable]
    public class SaveData
    {
        public Vector3 position;
        public int jumpCount;
        public float timer;
        public bool isTimerRunning;

        public SaveData(Vector3 playerPosition, float gameTimer, bool timerRunning)
        {
            position = playerPosition;
            timer = gameTimer;
            isTimerRunning = timerRunning;
        }
    }
}
