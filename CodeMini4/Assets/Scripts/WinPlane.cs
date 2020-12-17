using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlane : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider is player Reference

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.gameManager.SpawnBall();
        }
    }

}
