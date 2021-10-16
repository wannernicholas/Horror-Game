using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{

	public float walkingBobSpeed = 14f;
	public float walkingBobbingAmount = .05f;
	public float runningBobSpeed = 18f;
	public float runningBobbingAmount = .1f;
	public CharController controller;

	float defaultPosY = 0;
	float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

    	float bobSpeed = walkingBobSpeed;
    	float bobAmount = walkingBobbingAmount;
    	if ( Input.GetKey("right shift") || Input.GetKey("left shift") ){
    		bobSpeed = runningBobSpeed;
    		bobAmount = runningBobbingAmount;
    	}
        if(Mathf.Abs(controller.moveDirection.x) > 0.1f || Mathf.Abs(controller.moveDirection.z) > 0.1f)
        { 
            //Player is moving
            timer += Time.deltaTime * bobSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobAmount, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * runningBobSpeed), transform.localPosition.z);
        }
    }
}
