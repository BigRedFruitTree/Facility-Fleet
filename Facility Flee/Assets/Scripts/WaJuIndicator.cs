using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaJuIndicator : MonoBehaviour
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
        if (playerController.curWallJumps == 0)
        {
            GetComponent<Image>().color = Color.red;
        }
       
        if (playerController.curWallJumps == 1)
        {
            GetComponent<Image>().color = Color.yellow;
        }

        if (playerController.curWallJumps == 2)
        {
            GetComponent<Image>().color = Color.green;
        }

        if (playerController.curWallJumps == 3)
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}