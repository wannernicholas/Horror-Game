using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public float pickupRange = 4.0f;

    public Text tooltip;
	public GameObject monster;
	public GameObject deathScreen;
	public bool isDead;

    public AudioClip walkingSound;
    public AudioClip runningSound;
    private AudioSource audioSrc;

	private MonterController monsterController;
    private bool activlyPursued = false;

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
        deathScreen.SetActive(false);
        isDead = false;	
        tooltip.text = ""; 
        audioSrc = GetComponent<AudioSource>();
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

            //Stuff for pickup Basically, if the ray cast hits an object, then add it to inventory and banish it to the shadow realm
            RaycastHit infront;
            //Prob need to adjust the 10.0f to make it a good grab range.
            if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.rotation * Vector3.forward, out infront, pickupRange)){
            	GameObject x = infront.collider.gameObject;
            	if (x.tag.Equals("PickUp")){
                    tooltip.text = "Left Click to pick up"; 
                    if (Input.GetMouseButtonDown(0)){
            		  inventory.Add(x);
            		  x.transform.position = new Vector3(999.0f,999.0f,999.0f);
                      monsterController.BookPickedUp(this.transform.position);
                      if(x.name.Equals("Final Pickup")){
                        activlyPursued = true;
                        tooltip.text = "RUN";
                        //tooltip.GetComponent<ToolTipSaysGTFO>().RUN();
                      }
                      //TODO MAYBE INCREASE OUTLINE WHEN YOU CAN PICK IT UP?
                    }
            	}

            	else if (x.tag.Equals("Receptor")){
                    tooltip.text = "Left Click to drop off";
            		if (Input.GetMouseButtonDown(0)){
                        if(inventory.Count >0){
            			     Receptor y = x.GetComponent<Receptor>();
            			     y.AddBook(inventory[0]);
            			     inventory.RemoveAt(0);
                        }
                         
            		}
            	}
            }

            else if(!activlyPursued){
                tooltip.text = ""; 
            }
      
            // Crawling
            if ( Input.GetKey("space") ) {
            
            	//change scal
            	this.transform.localScale = new Vector3(1.0f,0.1f,1.0f);
            	curSpeedX = canMove ? crawlSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? crawlSpeed * Input.GetAxis("Horizontal") : 0;
                if( ((Mathf.Abs(curSpeedX) >0) || (Mathf.Abs(curSpeedY) > 0)) && (!audioSrc.isPlaying) ){
                    audioSrc.clip = walkingSound;
                    audioSrc.volume = Random.Range(0.05f,0.2f);
                    audioSrc.pitch = Random.Range(0.8f,1.1f);
                    audioSrc.Play();
                    //monsterController.Alert(this.transform.position);
                }
            }
            //if crouching but not holding button. 
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
                if( ((Mathf.Abs(curSpeedX) >0) || (Mathf.Abs(curSpeedY) > 0)) && (!audioSrc.isPlaying) ){
                    audioSrc.clip = walkingSound;
                    audioSrc.volume = Random.Range(0.05f,0.2f);
                    audioSrc.pitch = Random.Range(0.8f,1.1f);
                    audioSrc.Play();
                    //monsterController.Alert(this.transform.position);
                }
            }
            //If Running
            else if ( Input.GetKey("right shift") || Input.GetKey("left shift")  & !Input.GetKey("space") ){
            	curSpeedX = canMove ? runSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? runSpeed * Input.GetAxis("Horizontal") : 0;
            	//If Moving alert monster
            	if( (Mathf.Abs(curSpeedX) >0) || (Mathf.Abs(curSpeedY) > 0) ){
            		monsterController.Alert(this.transform.position);
                    if (!audioSrc.isPlaying){
                        audioSrc.clip = runningSound;
                        audioSrc.volume = Random.Range(0.2f,0.3f);
                        audioSrc.pitch = Random.Range(0.95f,1.1f);
                        audioSrc.Play();
                    }
            	}
            }

            //If Walking
            else {
            	curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
            	curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
                if( ((Mathf.Abs(curSpeedX) > 0) || (Mathf.Abs(curSpeedY) > 0)) && (!audioSrc.isPlaying) ){
                    audioSrc.clip = walkingSound;
                    audioSrc.volume = Random.Range(0.5f,0.2f);
                    audioSrc.pitch = Random.Range(0.8f,1.1f);
                    audioSrc.Play();
                    //monsterController.Alert(this.transform.position);
                }
            }

            if(activlyPursued){
                tooltip.text = "RUN";
                monsterController.Alert(this.transform.position);
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

    IEnumerator WaitThenDeactivate(GameObject deactivethis)
    {
        yield return new WaitForSeconds(5);
        Object.Destroy(deactivethis.gameObject);

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
     			deathScreen.SetActive(true);
     			LockMovement();
     			isDead = true;
     			Cursor.visible = true;
        		Cursor.lockState = CursorLockMode.None;
     		}
     	}
        else if(collision.gameObject.tag == "KnockOver"){
            monsterController.Alert(this.transform.position);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //PLAY SOUND OF CREECKING FLOOR
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //StartCoroutine(WaitThenDeactivate(collision.gameObject));
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
