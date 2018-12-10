using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseScript : MonoBehaviour {

    [SerializeField]
    protected AudioSource source; //Reference to audioSource, so that the character can play audios

    [SerializeField]
    protected AudioClip[] soundClips; //Reference to array of sounds, going to be used for animation sounds

    [SerializeField]
    protected float speed; //Float value to use as value for movement speed

    [SerializeField]
    protected float HitPoints; //How many lives the character has

    [SerializeField]
    protected float Damage; //How much damage they can deal in one hit

    protected Rigidbody2D Mybody; //Reference to the RigidiBody

    protected Collider2D collider; //Reference to the collider

    protected Vector3 change;  //Vector3 that will hold data regarding where the character is going (direction)

    protected Animator animator; //Reference to the animator

    protected virtual void Awake()
    {
        Mybody = gameObject.AddComponent<Rigidbody2D>(); //Adds RigidBody2D
        source = gameObject.AddComponent<AudioSource>(); //Adds AudioSource
        Mybody.gravityScale = 0; //Set the body not to be influenced by gravity
        Mybody.constraints = RigidbodyConstraints2D.FreezeRotation; //Freeze the rotation of the body
        animator = GetComponentInChildren<Animator>(); //Get reference to the animator
    }

    // Virtual Start to be modified in the derived classes
    protected virtual void Start ()
    {
		
	}

    //General method to make a character move around
    protected void MoveCharacter()
    {
        change.Normalize(); //Takes the abs value of the Vector3
        Mybody.MovePosition(transform.position + change * speed * Time.deltaTime); //Moves the body position towards the "change" (can be determined by player input or by code) multiplied by the speed value and deltatime
    }

    protected void Die()
    {
        gameObject.SetActive(false); //General Function to delete an enemy when it has no health left, SetActive is used so that we don't have to worry about the garbage collection of .NET

    }

    //Virtual method to be modified in the derived classes for their animations
    protected virtual void AnimatorController()
    {
       
    }
}
