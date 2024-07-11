using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public static int saveTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveGame()
    {
        saveTime = GameObject.Find("Player").GetComponentInChildren<LevelTime>().time;
        saveTime = (int) Mathf.Round(saveTime / 10);
        string activeSceneIndex = SceneManager.GetActiveScene().name + " bestTime";

        if (PlayerPrefs.HasKey(activeSceneIndex))
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " bestTime") > saveTime)
            {
                PlayerPrefs.SetInt(activeSceneIndex, saveTime);
            }
        }
        else
        {
            PlayerPrefs.SetInt(activeSceneIndex, saveTime);
        }

        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
