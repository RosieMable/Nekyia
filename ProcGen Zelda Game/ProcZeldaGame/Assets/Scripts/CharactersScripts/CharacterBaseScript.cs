using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseScript : MonoBehaviour {

    [SerializeField]
    protected float speed;

    protected Rigidbody2D body;

    protected Collider2D collider;

    protected Vector3 change;

    protected Animator animator;

    private void Awake()
    {
        body = gameObject.AddComponent<Rigidbody2D>();
        body.gravityScale = 0;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}

    protected void MoveCharacter()
    {
        body.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    protected virtual void AnimatorController()
    {
       
    }
}
