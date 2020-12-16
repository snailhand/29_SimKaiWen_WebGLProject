using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject Player;
    public float gravityMod = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMod;
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
