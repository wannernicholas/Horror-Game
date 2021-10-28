using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipSaysGTFO : MonoBehaviour
{
	private float ogX,ogY; 
    public float amplitude = 1000.5f;
    public float frequency = 1f;
    private bool run = false;
    // Start is called before the first frame update

    void Start()
    {
        ogX = this.transform.position.x;
        ogY = this.transform.position.y;
        //RUN();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(run){
        	Vector3 temp = this.transform.position;
            temp.x = ogX +Mathf.Sin ( Time.fixedTime *Mathf.PI * frequency) * amplitude;
            temp.y = ogY +Mathf.Sin ( Time.fixedTime *Mathf.PI * frequency) * amplitude;
            //temp.y -= fallSpeed;
            this.transform.position = temp;
            //Debug.Log("RUNing");
        }
    }
	*/
    public void RUN()
    {
    	Debug.Log("RUN");
    	this.GetComponent<Text>().text = "RUN";
    	run = true;
    }
}
