using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class Hero : CharacterBaseScript {

   public PlayerState currentState;
   public FloatValue currentHealth;
    public Signal PlayerHealthSignal;


    protected override void Start()
    {
        base.Start();
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update () {

        GetInput();

        HitPoints = currentHealth.initialValue;
	}

    void GetInput()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine("AttackCourutine");
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            AnimatorController();
        }
        else if (currentState == PlayerState.stagger)
        {
            StartCoroutine("StaggerCouroutine");
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

    private IEnumerator StaggerCouroutine()
    {
        animator.SetBool("stagger", true);
        yield return null;

        animator.SetBool("stagger", false);
        yield return new WaitForSeconds(0.5f);

        currentState = PlayerState.walk;
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

    public void KnockPlayer(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        PlayerHealthSignal.Raise();

        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            Die();
        }


    }


    private IEnumerator KnockCo(float knockTime)
    {
        if (Mybody != null)
        {
            yield return new WaitForSeconds(knockTime);
            Mybody.isKinematic = true;
            Mybody.velocity = Vector2.zero;
            

            //After the knock back, changes the state of the enemy
            currentState = PlayerState.idle;
            Mybody.isKinematic = false;
        }
    }
}
