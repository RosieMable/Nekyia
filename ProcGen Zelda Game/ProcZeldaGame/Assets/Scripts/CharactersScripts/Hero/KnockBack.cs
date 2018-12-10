using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {

    [SerializeField]
    private float thrust; // Impulse force to push enemy

    [SerializeField]
    private float knockTime; //Float value to define how long the knockback is

    public float damage; //Reference to the damage

    private void OnTriggerEnter2D(Collider2D other)
    {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>(); //Gets the rigid body of the collided object

            if (hit != null) // if the rigidbody is not null
            {
                Vector2 differnce = hit.transform.position - transform.position; //Calculate the difference between the object and the collided one
                differnce = differnce.normalized * thrust; //Takes the abs value of the difference and multiplies it by the thrust value
                hit.AddForce(differnce, ForceMode2D.Impulse); //Add force to the collided object, so it is pushed away

                if (hit.gameObject.CompareTag("Enemy") && other.isTrigger) //If the collided object is an enemy (PC Attack)
                {
                    //getting a reference to the FMS of the enemy, to set its current state as staggered when hit by the hero's sword
                    hit.GetComponent<EnemyBaseScript>().currentState = EnemyState.stagger;
                    other.GetComponent<EnemyBaseScript>().Knock(hit, knockTime, damage); //Calls the enemy's knock method
                    
                }
               else if (hit.gameObject.CompareTag("Player")) //If the collided object is the player (Enemy Attack)
                {
                if (other.GetComponent<Hero>().currentState != PlayerState.stagger) //If the hero state is not stagger
                {
                    hit.GetComponent<Hero>().currentState = PlayerState.stagger; //Set the state to stagger
                    hit.GetComponent<Hero>().KnockPlayer(knockTime, damage); //Call the Knock Player method
                }

                    
               }

            }
    }

}
