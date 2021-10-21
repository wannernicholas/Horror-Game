using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    	if ( Input.GetButtonDown("Cancel") & !playerController.isDead ){
        	if(!paused){
        		Pause();
        	}
        	
        	else{
        		UnPause();
        	}

        }
    }
    

    public void Pause()
    {
    	Time.timeScale = 0;
        paused = true;
        playerController.LockMovement();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
    }


    public void UnPause()
    {
    	Time.timeScale = 1;
        paused = false;
        playerController.UnlockMovement();
        Cursor.visible = false;
       	Cursor.lockState = CursorLockMode.Locked;
       	AudioListener.pause = false;
       	pauseMenu.SetActive(false);
        
    }

    public void ReloadScene()
    {
    	UnPause();
    	Scene temp = SceneManager.GetActiveScene();
        SceneManager.LoadScene(temp.name);
        StartCoroutine(WaitASec());
        //yield return new WaitForSeconds(1);
        SceneManager.SetActiveScene( temp );
    }
    IEnumerator WaitASec()
    {
    	yield return 0;
    }
}
