using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : CharacterBaseScript {

    private PlayerState currentState;

    public enum PlayerState
    {
        walk,
        attack,
        interact
    }

    protected override void Start()
    {
        base.Start();
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update () {

        GetInput();
        
	}

    void GetInput()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine("AttackCourutine");
        }
        else if (currentState == PlayerState.walk)
        {
            AnimatorController();
        }
    }

    protected override void AnimatorController()
    {
       

        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

   private IEnumerator AttackCourutine()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.5f);

        currentState = PlayerState.walk;
    }
}
