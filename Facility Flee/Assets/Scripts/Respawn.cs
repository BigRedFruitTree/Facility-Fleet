using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bad"))
        {
            transform.position = spawn.transform.position;

            GameObject.Find("Canvas").GetComponent<LevelTime>().time = 0;
            
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
