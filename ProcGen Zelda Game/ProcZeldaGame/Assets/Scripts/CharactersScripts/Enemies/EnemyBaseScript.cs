using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState //Public enumerator that will be used to determine the AI states
{
    idle,
    walk,
    attack,
    stagger
}

public class EnemyBaseScript : CharacterBaseScript
{

    public EnemyState currentState; //Reference to the current state of the enemy

    [SerializeField]
    protected string EnemyName; //String containing the name of the enemy

    [SerializeField]
    protected Transform target; //Transform that will hold the position of the target, which the enemy will move towards

    [SerializeField]
    protected float ChaseRadius; //Float value to determine the chase radius -> within this radius the enemy state will be "walking"

    [SerializeField]
    protected float attackRadius; //Float value to determine the chase attack -> within this radius the enemy state will be "attacking"

    public FloatValue maxHealth; //Reference to the scriptable object that holds the health of the enemy

    protected override void Awake() //Calls the base script
    {
        base.Awake();
    }

    protected override void Start()
    {
        if (target == null) //Sets the target if it is null
        {
            StartCoroutine(SetTarget());
        }

        HitPoints = maxHealth.initialValue; //Set the hit points to be equal to the gloatvalue
    }

    //Coroutine that looks for the target, until it hasn't been found
    IEnumerator SetTarget()
    {
        Hero tGO;
        do
        {
            tGO = FindObjectOfType<Hero>();

            if (tGO != null)
            {
                target = FindObjectOfType<Hero>().transform;
            }
            yield return new WaitForSeconds(1);
        } while (target == null);
    }

    //Method to quickly change the current state of the enemy
    protected void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    //Methods that determine the enemy behaviour depending on its distance from the PC
    protected void CheckDistance()
    {
        if (target == null) return; //If there isn't a target, stop

        //Calculates the distance between the target and the enemy positions, 
        //if this is less than the chase radius value but grater than the attack radius value, then the enemy starts moving towards the target
        if (Vector3.Distance(target.position, transform.position) <= ChaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            // Dont want to move the enemy while they are in attack or stagger state
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                //Move the enemy towards the target
                Vector3 tempPos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                //The precise amount of how much we are moving
                ChangeAnimation(tempPos - transform.position);

                Mybody.MovePosition(tempPos);



                //Changes the current state of the enemy to walk state -> Animation purposes
                ChangeState(EnemyState.walk);

            }

        } //If the enemy is within attack range, then...
        else if (Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            ChangeState(EnemyState.attack); //Change state to attack
            Attack(); //Calls the method attack
        }
        else
        {
            ChangeState(EnemyState.idle); //Changes states to dile
        }
    }

    private void TakeDamage(float damage) //Method that will be called whenever a knockback occours
    {
        HitPoints -= damage; //Damage - current health
        if (HitPoints <= 0) //If no health left
        {
            Die(); //...the enemy dies
        }
    }

    public void Knock(Rigidbody2D MyRigidbody, float knockTime, float damage) // Method called in the knockback script whenever the enemy gets hit
    {
        StartCoroutine(KnockCo(MyRigidbody, knockTime)); //Starts the coroutine for the stagger
        TakeDamage(damage); //Calls the take damage method
    }


    private IEnumerator KnockCo(Rigidbody2D MyRigidBody, float knockTime) //Coroutine that is started immediately after the thrust for the knockback has been applied
    {
        if (MyRigidBody != null) //The coroutine takes a rigidbody to manipulate and float value to determine the duration of the stagger state
        {
            animator.SetBool("stagger", true); //Reference to the animator (plays the stagger animation)
            yield return new WaitForSeconds(knockTime); //Waits for the knock time value before continuing
            MyRigidBody.velocity = Vector2.zero; //Set the rigid body velocity to zero, so it stops
            MyRigidBody.isKinematic = true; //Turn the kinematic bool to true, so that the body can not be moved, if not by script

            //After the knock back, changes the state of the enemy
            currentState = EnemyState.idle;
            animator.SetBool("stagger", false); //Finish the animation
            MyRigidBody.isKinematic = false;
        }
    }

    //Method to quickly assing the float values for the enemy blend tree
    protected void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

    //Method to easily change animation depending on which direction the enemy is facing
    protected void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > (Mathf.Abs(direction.y)))
        {
            //Going Right
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }

            else if (direction.x < 0) //Going Left
            {
                SetAnimFloat(Vector2.left);
            }
        }

        else if (Mathf.Abs(direction.x) < (Mathf.Abs(direction.y)))
        {
            if (direction.y > 0) // Going up
            {
                SetAnimFloat(Vector2.up);
            }

            else if (direction.y < 0) // Going down
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    protected void Attack() //Method that is called when the enemy is within attack range
    {
        StartCoroutine(AttackCourutine()); //Reference to the attack coroutine
    }

    //Coroutine that controls the attack animation and the attack state automatically for the enemies
    IEnumerator AttackCourutine()
    {
        animator.SetBool("attack", true); //Reference to the attack animation
        currentState = EnemyState.attack;
        yield return null;

        animator.SetBool("attack", false);
        yield return new WaitForSeconds(0.5f); //After the attack...

        currentState = EnemyState.walk; //Set the state back to walk
    }
}