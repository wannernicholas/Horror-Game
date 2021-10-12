using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterController : MonoBehaviour
{
	
	public GameObject startNode;


	private GameObject targetNode;
	private GameObject previousNode;
	private List<GameObject> possibleNodes;
	private float yPos;
	public float speed = 1.0f;
	
	[SerializeField]
	private List<float> speedUps = new List<float>();
	
    // Start is called before the first frame update
    void Start()
    {
    	yPos = this.transform.position.y;
        this.transform.position = new Vector3 (startNode.transform.position.x,
        										yPos,
        										startNode.transform.position.z);
        possibleNodes = startNode.GetComponent<MonsterNode>().connectedNodes;
        targetNode = possibleNodes[Random.Range(0,possibleNodes.Count)];
        previousNode = startNode;
        print("Start "+previousNode.name);
    }

    // Update is called once per frame
    void Update()
    {
    	
    	if ((this.transform.position.x == targetNode.transform.position.x ) & (this.transform.position.z ==targetNode.transform.position.z)) {
 
    		possibleNodes = targetNode.GetComponent<MonsterNode>().connectedNodes;
       		GameObject temp = targetNode;
       		//print("Prev "+ previousNode.name);
       		while (GameObject.ReferenceEquals(targetNode,previousNode)){
       			targetNode = possibleNodes[Random.Range(0,possibleNodes.Count)];
       			Debug.Log("Targetid "+targetNode.name +". prev "+previousNode.name);
       		}
       		if( GameObject.ReferenceEquals(targetNode,temp)){
       			previousNode =temp;
       		}
        	
        	Debug.Log(previousNode.name);
    		//previousNode = targetNode;
    		//Speed_Up();
    	}
    	else {
    		//TODO CHANGE THE POSITION VECTOR AND TARGET VECTOR SO IT DOSENT MOVE ON Y AXIS
    		this.transform.position = Vector3.MoveTowards(this.transform.position,targetNode.transform.position,speed * Time.deltaTime);
    	}
    	
    }

    void Speed_Up()
    {
    	if(speedUps.Count > 0){
    		speed = speedUps[0];
    		speedUps.RemoveAt(0);
    	}
    	
    }
}
