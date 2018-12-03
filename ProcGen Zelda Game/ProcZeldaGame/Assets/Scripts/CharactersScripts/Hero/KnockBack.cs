using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {

    [SerializeField]
    private float thrust; // Impulse force to push enemy

    [SerializeField]
    private float knockTime;

    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                Vector2 differnce = hit.transform.position - transform.position;
                differnce = differnce.normalized * thrust;
                hit.AddForce(differnce, ForceMode2D.Impulse);

                if (hit.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    //getting a reference to the FMS of the enemy, to set its current state as staggered when hit by the hero's sword
                    hit.GetComponent<EnemyBaseScript>().currentState = EnemyState.stagger;
                    other.GetComponent<EnemyBaseScript>().Knock(hit, knockTime, damage);
                    
                }
               else if (hit.gameObject.CompareTag("Player"))
                {
                if (other.GetComponent<Hero>().currentState != PlayerState.stagger)
                {
                    hit.GetComponent<Hero>().currentState = PlayerState.stagger;
                    hit.GetComponent<Hero>().KnockPlayer(knockTime, damage);
                }

                    
            }

        }
    }

}
