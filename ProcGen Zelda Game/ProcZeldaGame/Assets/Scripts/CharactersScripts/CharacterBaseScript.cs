using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseScript : MonoBehaviour {

    [SerializeField]
    protected AudioSource source;

    [SerializeField]
    protected AudioClip[] soundClips;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float HitPoints;

    [SerializeField]
    protected float Damage;

    protected Rigidbody2D Mybody;

    protected Collider2D collider;

    protected Vector3 change;

    protected Animator animator;

    protected void Awake()
    {
        Mybody = gameObject.AddComponent<Rigidbody2D>();
        source = gameObject.AddComponent<AudioSource>();
        Mybody.gravityScale = 0;
        Mybody.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
   protected virtual void Start () {
		
	}

    protected void MoveCharacter()
    {
        change.Normalize();
        Mybody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    protected void Die()
    {
        this.gameObject.SetActive(false);

    }

    protected virtual void AnimatorController()
    {
       
    }
}
