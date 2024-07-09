using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class TitleMenu : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject optionsPanel;
    public GameObject playerPrefab;
    private PlayerController playerController;
    public Slider slider;
    public TextMeshProUGUI sensitivityDisplay;

    // Start is called before the first frame update
    void Start()
    {
        playerController = playerPrefab.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPressed()
    {
        titlePanel.SetActive(false);
        SceneManager.LoadScene("Level");
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

    public void SliderChanged()
    {
        float amount = slider.value;
        playerController.mouseSens = amount;
        sensitivityDisplay.text = amount.ToString();
    }
}
