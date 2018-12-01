﻿using System.Collections;
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
    protected int HitPoints;

    [SerializeField]
    protected int Damage;

    protected Rigidbody2D body;

    protected Collider2D collider;

    protected Vector3 change;

    protected Animator animator;

    private void Awake()
    {
        body = gameObject.AddComponent<Rigidbody2D>();
        source = gameObject.AddComponent<AudioSource>();
        body.gravityScale = 0;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
   protected virtual void Start () {
		
	}

    protected void MoveCharacter()
    {
        change.Normalize();
        body.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    protected void Die()
    {
        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void AnimatorController()
    {
       
    }
}
