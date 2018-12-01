﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class EnemyBaseScript : CharacterBaseScript {

    public EnemyState currentState;

    [SerializeField]
    protected GameObject ItemDrop;

    [SerializeField]
    protected string EnemyName;

    [SerializeField]
    protected Transform target;

    [SerializeField]
    protected float ChaseRadius;

    [SerializeField]
    protected float attackRadius;

    [SerializeField]
    protected Transform homePosition;

    protected void Awake()
    {
        base.Awake();

        Mybody.isKinematic = true;
    }

    protected void Start()
    {
        target = FindObjectOfType<Hero>().transform;
    }

    protected void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    protected void CheckDistance()
    {
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

        }

        else
        {
            ChangeState(EnemyState.idle);
        }
    }

    public void Knock(Rigidbody2D MyRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(MyRigidbody, knockTime));
    }

    protected  IEnumerator KnockCo(Rigidbody2D MyRigidBody, float knockTime)
    {
        if (MyRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            MyRigidBody.velocity = Vector2.zero;
            MyRigidBody.isKinematic = true;

            //After the knock back, changes the state of the enemy
            currentState = EnemyState.idle;
        }
    }


    //Method to quickly assing the float values for the enemy blend tree
    protected void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

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

}
