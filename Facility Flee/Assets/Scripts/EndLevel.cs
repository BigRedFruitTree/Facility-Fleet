using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private GameObject player;
    private SaveData saveData;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        saveData = GameObject.Find("EmptySave").GetComponent<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // win level, save time if its best time, then move to next level
            saveData.SaveGame();
            SceneManager.LoadScene("UI SCENE");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
