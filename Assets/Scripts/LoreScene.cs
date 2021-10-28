using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoreScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Text tooltip;
	public string nextScene;
	public float waitForThisLong = 4.0f;
	private bool canContinue = false;
    // Start is called before the first frame update
    void Start()
    {
    	tooltip.text = "";
        StartCoroutine(WaitASec());
        
    }

    IEnumerator WaitASec()
    {
    	yield return new WaitForSeconds(waitForThisLong);
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
