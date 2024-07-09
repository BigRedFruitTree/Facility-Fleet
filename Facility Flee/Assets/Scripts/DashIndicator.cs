using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashIndicator : MonoBehaviour
{
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerController.curDashes == 0)
        {
            GetComponent<Image>().color = Color.red;
        } else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
