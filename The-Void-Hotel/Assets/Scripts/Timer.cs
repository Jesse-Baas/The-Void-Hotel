using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    public float minutes;
    public float seconds;
    public TextMeshProUGUI TimerTxt;
    public bool IsCounting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCounting == true)
        {
            minutes = Mathf.Floor(timer / 60);
            seconds = Mathf.Floor(timer % 60);

            TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                print("Game Over");
                IsCounting = false;
                TimerTxt.text = string.Format("Game Over!");
                DisablePlayerObjects();
            }
        }
    }
    void DisablePlayerObjects()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player_Char");

        foreach (GameObject playerObject in playerObjects)
        {
            MonoBehaviour[] scripts = playerObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }
    }
}
