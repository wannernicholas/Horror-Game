using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : MonoBehaviour
{
    public GameObject finalBook;
    public GameObject wallToRemove;
    private float ogX,ogZ; 
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float fallSpeed = 1f;
	[SerializeField]
	private List<GameObject> positions = new List<GameObject>();
	private bool[] isPlaced;
    private bool full;

    // Start is called before the first frame update
    void Start()
    {
        isPlaced = new bool[positions.Count];
        int i = 0;
        while (i< positions.Count){
        	isPlaced[i] = false;
        	i = i+1;
        }
        full = false;
        finalBook.SetActive(false);
        ogX = wallToRemove.transform.position.x;
        ogZ = wallToRemove.transform.position.z;
    }

    void Update()
    {
        if (full){
            Vector3 temp = wallToRemove.transform.position;
            temp.x = ogX +Mathf.Sin ( Time.fixedTime *Mathf.PI * frequency) * amplitude;
            temp.z = ogZ +Mathf.Sin ( Time.fixedTime *Mathf.PI * frequency) * amplitude;
            temp.y -= fallSpeed;
            wallToRemove.transform.position = temp;
            //PLAY RUMBLE SOUND EFFECT
            if(wallToRemove.transform.position.y <= -5.25){
                full = false;
                //Debug.Log("At Bottom");
            }
        }
    }
    // adds object and moves it into position
    public void AddBook(GameObject newBook)
    {
    	//I know this is bad practice and we'll have to make sure the names match but ¯\_(ツ)_/¯
    	//GameObject temp = Instantiate(newBook);
        //Vector3 temp = new Vector3(-0.7071068f, 0.0f,0.0f,0.7071067f);
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.Set(-0.7071068f, 0.0f,0.0f,0.7071067f);
        newBook.transform.rotation =newQuaternion;
    	if(newBook.name.Equals("Pickup0")){
    		newBook.transform.position = positions[0].transform.position;
    		isPlaced[0] = true;
    	}
    	else if (newBook.name.Equals("Pickup1")){
    		newBook.transform.position = positions[1].transform.position;
    		isPlaced[1] = true;
    	}
    	else if (newBook.name.Equals("Pickup2")){
    		newBook.transform.position = positions[2].transform.position;
    		isPlaced[2] = true;
    	}
    	else if (newBook.name.Equals("Pickup3")){
    		newBook.transform.position = positions[3].transform.position;
    		isPlaced[3] = true;
    	}
    	else if (newBook.name.Equals("Pickup4")){
    		newBook.transform.position = positions[4].transform.position;
    		isPlaced[4] = true;
    	}
    	else{
    		Debug.Log("Some name is messed up here "+newBook.name);
    		return;
    	}
    	newBook.tag = "Untagged"; 
    	checkIfFiiled();
    }
    

    void checkIfFiiled()
    {
    	foreach (bool i in isPlaced){
    		if (!i){
    			return;
    		}
    	}

    	//Debug.Log("Its full now");
        this.tag = "Untagged";
        finalBook.SetActive(true);
        full= true;
    		/*
			~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
				TODO: UNLOCK EXIT DOOR
			~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
     		*/
    }
}
