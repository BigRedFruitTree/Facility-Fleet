using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelTime : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI bestTimeDisplay;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        time =  0;

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + " bestTime"))
        {
            bestTimeDisplay.text = "Best Level Time: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " bestTime");
        }
        else
        {
            bestTimeDisplay.text = "Best Level Time: N/A";
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += 1;
        timeDisplay.text = "Time: " + Mathf.Round(time / 10).ToString();

    }
}
