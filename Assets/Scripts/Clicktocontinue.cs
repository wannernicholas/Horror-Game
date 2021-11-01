using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clicktocontinue : MonoBehaviour
{
	public Text tooltip;
	public string nextScene;
	private bool canContinue = false;
    // Start is called before the first frame update
    void Start()
    {
    	tooltip.text = "";
        StartCoroutine(WaitASec());
        
    }

    IEnumerator WaitASec()
    {
    	yield return new WaitForSeconds(2);
    	canContinue = true;
    	tooltip.text = "Left click to continue";
    }
    // Update is called once per frame
    void Update()
    {
        if(canContinue){
        	if (Input.GetMouseButtonDown(0)){
        		SceneManager.LoadScene(nextScene);
        	}
        }
    }
}
