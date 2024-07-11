using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaJuIndicator : MonoBehaviour
{
    private PlayerController playerController;

    public List<GameObject> spriteList;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in spriteList)
        {
            item.SetActive(false);
        }

        spriteList[playerController.curWallJumps].SetActive(true);
    }
}