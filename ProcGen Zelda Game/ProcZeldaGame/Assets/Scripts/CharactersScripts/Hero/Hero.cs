using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : CharacterBaseScript {

	
	// Update is called once per frame
	void Update () {

        GetInput();
        MoveCharacter();
        AnimatorController();
        
	}

    void GetInput()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
    }

    protected override void AnimatorController()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
