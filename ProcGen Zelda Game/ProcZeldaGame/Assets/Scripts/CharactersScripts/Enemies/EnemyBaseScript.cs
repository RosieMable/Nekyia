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

public class EnemyBaseScript : CharacterBaseScript {

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

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        if (target == null)
        {
            StartCoroutine(SetTarget());
        }

        HitPoints = maxHealth.initialValue;
    }

    IEnumerator  SetTarget()
    {
        Hero tGO;
        do
        {
            tGO=FindObjectOfType<Hero>();

            if (tGO!=null)
            {
                target = FindObjectOfType<Hero>().transform;
            }
            yield return new WaitForSeconds(1);
        } while (target == null);
    }

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
        if (target == null) return;
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
            ChangeState(EnemyState.attack);
            Attack();
        }
        else
        {
            ChangeState(EnemyState.idle);
        }
    }

    private void TakeDamage(float damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            Die();
        }
    }

    public void Knock(Rigidbody2D MyRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(MyRigidbody, knockTime));
        TakeDamage(damage);
    }


    private  IEnumerator KnockCo(Rigidbody2D MyRigidBody, float knockTime)
    {
        if (MyRigidBody != null)
        {
            animator.SetBool("stagger", true);
            yield return new WaitForSeconds(knockTime);
            MyRigidBody.velocity = Vector2.zero;
            MyRigidBody.isKinematic = true;

            //After the knock back, changes the state of the enemy
            currentState = EnemyState.idle;
            animator.SetBool("stagger", false);
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

    protected void Attack()
    {
        StartCoroutine(AttackCourutine());
    }

    //Coroutine that controls the attack animation and the attack state automatically for the enemies
    IEnumerator AttackCourutine()
    {
        animator.SetBool("attack", true);
        currentState = EnemyState.attack;
        yield return null;

        animator.SetBool("attack", false);
        yield return new WaitForSeconds(0.5f);

        currentState = EnemyState.walk;
    }

}
