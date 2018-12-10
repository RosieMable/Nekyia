using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitDoor : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hero>())
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(3);
    }
}
