using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]

public class CharController : MonoBehaviour
{

	public float walkSpeed = 7.5f;
	public float runSpeed = 9.0f;
	//public float jumpSpeed = 8.0f
	public float gravity = 20.0f;
	public Camera playerCamera;
	public float lookSpeed = 2.0f;
	public float lookXLimit = 45.0f;

	CharacterController characterController;
	public Vector3 moveDirection = Vector3.zero;
	Vector2 rotation = Vector2.zero;
	private bool canHide = false;
	public bool canMove = true; 

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded){
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX;
            float curSpeedY;
            if ( Input.GetKey("right shift") || Input.GetKey("left shift") ){
            	curSpeedX = canMove ? runSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? runSpeed * Input.GetAxis("Horizontal") : 0;
            }
            else {
            	curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
            }
            
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //Prob not gonna need to jump
            //if (Input.GetButton("Jump") && canMove)
            //{
            //    moveDirection.y = jumpSpeed;
            //}
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove){
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }

    void LockMovement()
    {
    	canMove = false;
    }
    void UnlockMovement()
    {
    	canMove = true;
    }
}
