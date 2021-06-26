using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController controller;
	public float walkSpeed = 12f;
	public float runSpeed = 20f;
	public float gravity = -9.81f;
	public Transform groundCheck;
	public float groundDistance = .4f;
	public LayerMask groundMask;

	Vector3 velocity;
	public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
    	isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    	if(isGrounded && velocity.y <0)
    	{
    		velocity.y = -2f;
    	}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right* x) + (transform.forward *z);
        if (Input.GetKey("right shift") || Input.GetKey("left shift"))
        {
        	controller.Move(move * runSpeed * Time.deltaTime);
        }
        else
        {
        	controller.Move(move * walkSpeed * Time.deltaTime);
        }
        
        velocity.y +=  gravity * Time.deltaTime;

        controller.Move(velocity* Time.deltaTime);
    }
}
