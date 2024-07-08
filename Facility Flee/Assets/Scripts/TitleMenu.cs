using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPressed()
    {
        // Add code to start game.
    }

    public void OptionsPressed()
    {
        titlePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void ExitOptionsPressed()
    {
        optionsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}
