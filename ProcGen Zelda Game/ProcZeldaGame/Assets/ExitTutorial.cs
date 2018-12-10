using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorial : MonoBehaviour {

    [SerializeField]
    private Signal ExitSignal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hero>())
        {
            ExitSignal.Raise();
        }
    }

}
