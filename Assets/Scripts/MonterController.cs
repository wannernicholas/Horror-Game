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
	public float speed = 7.0f;
	public float huntingSpeed = 10.0f;
	public float searchTime = 10.0f;
	public float minTeleportDist = 30.0f;
	public float maxTeleportDist = 60.0f;
	private float timeLeft;

    public List<AudioClip> chainSfx = new List<AudioClip>();
    private AudioSource audioSrc;

	private Vector3 huntingPos;

	private bool hunting = false;
	private bool searching = false;
	private bool roaming = true;
	private bool gaveUp = false;

	private GameObject[] nodeList;

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
        //print("Start "+previousNode.name);
        nodeList = GameObject.FindGameObjectsWithTag("Node");
        timeLeft = searchTime;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (roaming){
    		Roam();
    	}
    	if (hunting){
    		Hunt();
    	}
    	if(!audioSrc.isPlaying){
            audioSrc.clip = chainSfx[Random.Range(0,chainSfx.Count)];
            audioSrc.volume = Random.Range(0.3f,0.5f);
            audioSrc.pitch = Random.Range(0.8f,1.1f);
            audioSrc.Play();
        }
    }

    //Called by other objects, changes it to hunting mode
    public void Alert(Vector3 pos)
    {
    	roaming = false;
    	huntingPos = pos;
    	hunting = true;
    	searching = false;
    	gaveUp = false;
    }

    //Happens When it recives a location to check out
    void Hunt()
    {
    	//When it gets there it switches to searching mode
    	if ((this.transform.position.x == huntingPos.x ) & (this.transform.position.z ==huntingPos.z) & !searching) {
    		searching = true;
    	}
    	//If its in searching mode and hasnt given up it will circle for searchTime before giving up.
    	if (searching & !gaveUp){
			Vector3 v = new Vector3 (Mathf.Cos(timeLeft)*.05f, 0.0f, Mathf.Sin(timeLeft)*.05f);
			this.transform.position = this.transform.position+ v*Time.deltaTime;
    		timeLeft -= Time.deltaTime;
    		if(timeLeft <= 0){
    			timeLeft = searchTime;
    			gaveUp = true;
    		}
    	}
    	//When it gives up it finds the closest node and moves to there before switching back to roaming mode.
    	if (gaveUp){
    		float dist = float.PositiveInfinity;
    		Vector3 pos = this.transform.position;
    		GameObject temp = nodeList[0];
    		float d;
    		foreach (var obj in nodeList){
    			d = (pos - obj.transform.position).sqrMagnitude;
    			if (d < dist){
    				temp = obj;
    				dist = d;
    			}
    		}
            previousNode = null;
    		targetNode = temp;
    		roaming = true;
    		hunting = false;
    		gaveUp = false;
    		searching = false;
    	}
    	//just moving it towards its position
    	else{
            //Vector3 targetDirection = target.position - transform.position;
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed*Time.deltaTime, 0.0f);
    		//transform.rotation = Quaternion.LookRotation(newDirection);
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(huntingPos.x,yPos,huntingPos.z), huntingSpeed * Time.deltaTime);
    	}
    }

    //Base behavior when roaming through scene
    void Roam()
    {
    	//It it reaches its location of the next node it will randomly select a node except for the one it came from
    	if ((this.transform.position.x == targetNode.transform.position.x ) & (this.transform.position.z ==targetNode.transform.position.z)) {
 
    		possibleNodes = new List<GameObject> (targetNode.GetComponent<MonsterNode>().connectedNodes);
       		GameObject temp = targetNode;
       		for (int i = 0; i < possibleNodes.Count; i++){
       			if (GameObject.ReferenceEquals(possibleNodes[i],previousNode)){
       				possibleNodes.RemoveAt(i);
       			} 
       		}
       			targetNode = possibleNodes[Random.Range(0,possibleNodes.Count)];
       			previousNode =temp;
    	}
    	//If its not there keep moving
    	else {
            //Vector3 targetDirection = target.position - transform.position;
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed*Time.deltaTime, 0.0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
    		this.transform.position = Vector3.MoveTowards(this.transform.position,new Vector3(targetNode.transform.position.x,yPos,targetNode.transform.position.z),speed * Time.deltaTime);
    	}
    }

    //Changes speed to next speed in the table
    public void BookPickedUp(Vector3 pos)
    {
    	if(speedUps.Count > 0){
    		speed = speedUps[0];
    		speedUps.RemoveAt(0);
    	}
        /* Unfortunatley I dont think this is worth it
    	possibleNodes = new List<GameObject> ();
    	float d;
    	foreach (var obj in nodeList){
    		d = (pos - obj.transform.position).sqrMagnitude;
    		if (d > minTeleportDist && d < maxTeleportDist){
    			possibleNodes.Add(obj);
    		}
    	}
    	if (possibleNodes.Count > 0)
    	{
    		targetNode = possibleNodes[Random.Range(0,possibleNodes.Count)];
    		this.transform.position = targetNode.transform.position;
    	}
        */
    	
    }
}
