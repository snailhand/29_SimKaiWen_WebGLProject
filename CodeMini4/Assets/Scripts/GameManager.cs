using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject ballObject;
    public GameObject Player;

    public float gravityMod = 3f;
    public bool isWaiting;
    public int count = 0;

    private float ballSpeed = 100f;
    private int ballLimit = 1000;
    private bool ballSpawn = false;

    void Start()
    {
        Physics.gravity *= gravityMod;
        if (gameManager == null)
        {
            gameManager = this;
        }
    }
    public void SpawnBall()
    {
        ballSpawn = true;

    }

    // Update is called once per frame
    void Update()
    {
        //ballObject.transform.Translate(Vector3.forward * Time.deltaTime * ballSpeed);

        if (ballSpawn == true)
        {
            Vector3 spawnPosition = new Vector3(-2, 54, 700);
            Debug.Log(Time.frameCount);


            if (count < ballLimit && Time.frameCount % 300 == 0)
            {
                float direction = 160;
                int randomDir = Random.Range(0, 3);
                switch (randomDir)
                {
                    case 0:
                        direction = 140;
                        break;
                    case 1:
                        direction = 180;
                        break;
                    case 2:
                        direction = 220;
                        break;
                }

                Instantiate(ballObject, spawnPosition, Quaternion.Euler(0,direction,0));
                count++;
            }
        }
    }



}
