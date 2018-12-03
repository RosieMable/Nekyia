using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBaseScript {

    // Use this for initialization
    private void Start()
    {
        base.Start();
        currentState = EnemyState.idle;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (target == null)
        {
            target = FindObjectOfType<Hero>().transform;
        }


        CheckDistance();
    }

}
