using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    [SerializeField]
    private int int_gameScene = 1;


    public void StartButton()
    {
        SceneManager.LoadScene(int_gameScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
        
        
