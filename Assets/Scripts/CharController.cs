using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]

public class CharController : MonoBehaviour
{

	public float walkSpeed = 7.5f;
	public float runSpeed = 9.0f;
	public float crawlSpeed = 4.0f;
	//public float jumpSpeed = 8.0f
	public float gravity = 20.0f;
	public Camera playerCamera;
	public float lookSpeed = 2.0f;
	public float lookXLimit = 45.0f;
	public float pickupRange = 10.0f;

	public GameObject monster;
	private MonterController monsterController;

	CharacterController characterController;
	public Vector3 moveDirection = Vector3.zero;
	Vector2 rotation = Vector2.zero;
	public bool canMove = true; 

	private bool hidden = false;

	//Stuff for pickup
	
	//private List<GameObject> pickupsInRange = new List<GameObject>();
	public List<GameObject> inventory = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        monsterController = monster.GetComponent<MonterController>();
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

            //Stuff for pickup Basically, if the ray cast hits an object, then add it to inventory and destroy it
            RaycastHit infront;
            //Prob need to adjust the 10.0f to make it a good grab range.
            if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.rotation * Vector3.forward, out infront, pickupRange)){
            	GameObject x = infront.collider.gameObject;
            	//Debug.Log("Looking At something" + x.tag);
            	if ((x.tag.Equals("PickUp")) & (Input.GetMouseButtonDown(0))){
            		//Add Message saying click to pick up on canvas
            		inventory.Add(x);
            		x.transform.position = new Vector3(999.0f,999.0f,999.0f);
            		//Debug.Log("Added to Invetory"+inventory.Count+x.name);
            	}
            	else if ((x.tag.Equals("Receptor")) & (Input.GetMouseButtonDown(0))){
            		if(inventory.Count >0)
            		{
            			//	display message saying something like "place book"
            			Receptor y = x.GetComponent<Receptor>();
            			y.AddBook(inventory[0]);
            			inventory.RemoveAt(0);
            		}
            	}
            }
      
            // Crawling is prob gonna be using the ctrl keys, the sticky key was just driving me crazy.
            //if ( Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ) {
            if ( Input.GetKey("space") ) {
            
            	//change scal
            	this.transform.localScale = new Vector3(1.0f,0.1f,1.0f);
            	curSpeedX = canMove ? crawlSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? crawlSpeed * Input.GetAxis("Horizontal") : 0;
            }
            //else if ( (this.transform.localScale != new Vector3(1.0f,1.0f,1.0f)) & !Input.GetKey(KeyCode.LeftControl) & !Input.GetKey(KeyCode.RightControl) ){
            else if ( (this.transform.localScale != new Vector3(1.0f,1.0f,1.0f)) & (!Input.GetKey("space")) ){
            
            	//change scal
            	RaycastHit vibeCheck;
            	//Basically if theres nothing above it, then it can return to normal
            	//This final value might need to be adjusted if its causing issues in the future
            	if(!Physics.Raycast(this.transform.position,Vector3.up, out vibeCheck,1.3f)){
            		this.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            	}
            	curSpeedX = canMove ? crawlSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? crawlSpeed * Input.GetAxis("Horizontal") : 0;

            }
            //If Running
            else if ( Input.GetKey("right shift") || Input.GetKey("left shift")  & !Input.GetKey("right ctrl") & ! Input.GetKey("left ctrl")){
            	curSpeedX = canMove ? runSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? runSpeed * Input.GetAxis("Horizontal") : 0;
            	//If Moving alert monster
            	if((curSpeedX >0) || (curSpeedY > 0)){
            		monsterController.Alert(this.transform.position);
            	}
            }

            //If Walking
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


    public void LockMovement()
    {
    	canMove = false;
    }


    public void UnlockMovement()
    {
    	canMove = true;
    }


    void OnTriggerEnter(Collider collision)
     {
     	if (collision.gameObject.tag == "HidingSpot"){
     		hidden = true;
     		//Debug.Log("Now Hidden");
     	}
     	else if (collision.gameObject.tag == "Death"){
     		if (!hidden){
     			Debug.Log("You dead boi");
     			/*
				~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
						TODO: IMPLEMENT DEATH
				~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
     			*/
     		}
     	}
     }

     void OnTriggerExit(Collider collision)
     {
     	if (collision.gameObject.tag == "HidingSpot"){
     		hidden = false;
     		//Debug.Log("No Longer Hidden");
     	}
     }
}
