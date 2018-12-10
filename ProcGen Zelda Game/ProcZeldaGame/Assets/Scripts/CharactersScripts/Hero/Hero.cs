using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState //Public enumerator that will be used to determine the PC states
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class Hero : CharacterBaseScript {

   public PlayerState currentState; //Reference to the enum
   public FloatValue currentHealth; //Reference to the signal-pattern system, it indicates the current health
    public Signal PlayerHealthSignal; //Signal used when the player gets hit, so that the UI can be updated


    protected override void Start()
    {
        base.Start(); //Calls the CharacterBase Script
        currentState = PlayerState.walk; //Sets the current state to walk
    }

    // Update is called once per frame
    void Update () {

        GetInput(); //Get input from the player to move the PC

        HitPoints = currentHealth.initialValue; //Base class value is assigned to the scriptable object one
	}

    void GetInput()
    {
        change = Vector3.zero; //Set the vector to zero
        change.x = Input.GetAxisRaw("Horizontal"); //Track the movements on the x axis
        change.y = Input.GetAxisRaw("Vertical"); //Track the movements on the y axis

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) //If pressed "attack" (space) then...
        {
            StartCoroutine(AttackCourutine()); //StartCoroutine attack
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle) //If the pc is either walking or in idle then...
        {
            AnimatorController(); //Handles the animations of the character
        }
        else if (currentState == PlayerState.stagger) //If the pc is staggered...
        {
            StartCoroutine(StaggerCouroutine()); //...StaggerCoroutine starts
        }
    }

    //Method that controls the animations of the pc
    protected override void AnimatorController()
    {
        //If the Input is not equal to null...
        if (change != Vector3.zero)
        {
            MoveCharacter(); //...Move the pc
            animator.SetFloat("moveX", change.x); //Reference to thee blend tree parameters
            animator.SetFloat("moveY", change.y); //Reference to thee blend tree parameters
            animator.SetBool("moving", true); //Reference to thee blend tree parameters (to go from idle to the walking tree)
        }
        else
        {
            animator.SetBool("moving", false); //Else, the animator goes to the idle state
        }
    }

    //Coroutine that plays the stagger animation
    private IEnumerator StaggerCouroutine()
    {
        animator.SetBool("stagger", true);
        yield return null;

        animator.SetBool("stagger", false);
        yield return new WaitForSeconds(0.5f); //Waits a few seconds before...

        currentState = PlayerState.walk; //Going back to the walk state
    }

    //Coroutine that plays the attack animation and puts the PC into the attack state
   private IEnumerator AttackCourutine()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.5f);

        currentState = PlayerState.walk;
    }

    //Coroutine that starts the knockback coroutine, registers the damage taken from the fight and raise the signal to the UI OR make the PC die 
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

    //Coroutine that determines the duration of the knockback
    private IEnumerator KnockCo(float knockTime)
    {
        if (Mybody != null)
        {
            yield return new WaitForSeconds(knockTime);
            Mybody.isKinematic = true;
            Mybody.velocity = Vector2.zero;
            

            //After the knock back, changes the state of the PC
            currentState = PlayerState.idle;
            Mybody.isKinematic = false;
        }
    }
}
