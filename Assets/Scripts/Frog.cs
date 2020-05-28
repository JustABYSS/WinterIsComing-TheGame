﻿using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] float leftCap;
    [SerializeField] float rightCap;

    [SerializeField] private GameObject leftComponent;
    [SerializeField] private GameObject rightComponent;

    [SerializeField] private float jumpLenght = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;
    private AudioSource dead;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        dead = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Transition from Jump to Fall
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        //Transition from Fall to Idle
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftComponent.GetComponent<Transform>().position.x)
            {
                //Make sure sprite is facing right location, and if it is not, then face the right direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //Test to see if I am on the ground, if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }

            else
            {
                facingLeft = false;
            }

        }

        else
        {
            if (transform.position.x < rightComponent.GetComponent<Transform>().position.x)
            {
                //Make sure sprite is facing right location, and if it is not, then face the right direction
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                //Test to see if I am on the ground, if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    //Jump
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    anim.SetBool("Jumping", true);

                }
            }

            else
            {
                facingLeft = true;
            }

        }
    }

    private void Dead()
    {
        dead.Play();
    }


   

   
}
