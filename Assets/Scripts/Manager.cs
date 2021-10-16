using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public GameObject player;
	public GameObject pauseMenu;
	private bool paused;
	private CharController playerController;
    // Start is called before the first frame update
    void Start()
    {
    	playerController = player.GetComponent<CharController>();
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);	
    }

    // Update is called once per frame
    void Update()
    {
        //if ( Input.GetKey(KeyCode.Escape) ){
    	if ( Input.GetButtonDown("Cancel") ){
        	if(!paused){
        		//THIS IS PAUSING THE GAME
        		Time.timeScale = 0;
        		paused = true;
        		playerController.LockMovement();
        		Cursor.visible = true;
        		Cursor.lockState = CursorLockMode.None;
        		AudioListener.pause = true;
        		pauseMenu.SetActive(true);
        		//do other stuff like make pause menu visible and put darkening filter over screen
        	}
        	
        	else{
        		//THIS UNPAUSES THE GAME
        		Time.timeScale = 1;
        		paused = false;
        		playerController.UnlockMovement();
        		Cursor.visible = false;
       			Cursor.lockState = CursorLockMode.Locked;
       			AudioListener.pause = false;
       			pauseMenu.SetActive(false);
        		//remove pause menu/ filter
        	}

        }
    }
}
