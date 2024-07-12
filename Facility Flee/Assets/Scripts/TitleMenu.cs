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
    public GameObject pausePanel;
    public TextMeshProUGUI resetButtonText;
    private PlayerController playerController;
    public Slider slider;
    public Slider sfxSlider;
    public Slider musicSlider;
    public TextMeshProUGUI sensitivityDisplay;
    public TextMeshProUGUI sfxVolumeDisplay;
    public TextMeshProUGUI musicVolumeDisplay;
    public TextMeshProUGUI bestTimeText;
    public bool playingGame;

    // Start is called before the first frame update
    void Start()
    {
        playerController = playerPrefab.GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.None;
        bestTimeText.text = "BEST TIME: " + SaveData.saveTime;
    }

    private bool debounce = false;
    public void ResetDataPressed()
    {
        if (debounce == false)
        {
            debounce = true;
            PlayerPrefs.DeleteAll();
            resetButtonText.text = "Data has been reset";
            StartCoroutine(DebounceWait());
        }
    }

    IEnumerator DebounceWait()
    {
        yield return new WaitForSeconds(2);
        resetButtonText.text = "Reset Data";
        debounce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPressed()
    {
        
        titlePanel.SetActive(false);
        SceneManager.LoadScene("Level2");
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
        sensitivityDisplay.text = playerController.mouseSens.ToString();
    }

    public void SfxSliderChanged()
    {
        AudioSource sfx = playerPrefab.GetComponent<AudioSource>();
        float amount = sfxSlider.value;
        sfx.volume = amount;
        sfxVolumeDisplay.text = sfx.volume.ToString();
    }

    public void MusicSliderChanged()
    {
        AudioSource music = playerController.cam.GetComponent<AudioSource>();
        float amount = musicSlider.value;
        music.volume = amount;
        musicVolumeDisplay.text = music.volume.ToString();
    }
    
}
