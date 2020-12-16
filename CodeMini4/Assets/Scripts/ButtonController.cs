using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
