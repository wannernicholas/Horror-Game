using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

	public float mouseSensitivity = 200f;
	public Transform playerBody;
	float xRot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot,-90f,90f); //this makes it so you cant look backwards by continously looking up

        transform.localRotation = Quaternion.Euler(xRot,0f,0f);
        playerBody.Rotate(Vector3.up *mouseX);
        
    }
}
