using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> positions = new List<GameObject>();
	private bool[] isPlaced;

    // Start is called before the first frame update
    void Start()
    {
        isPlaced = new bool[positions.Count];
        int i = 0;
        while (i< positions.Count){
        	isPlaced[i] = false;
        	i = i+1;
        }
    }

    // adds object and moves it into position
    public void AddBook(GameObject newBook)
    {
    	//I know this is bad practice and we'll have to make sure the names match but ¯\_(ツ)_/¯
    	//GameObject temp = Instantiate(newBook);
    	if(newBook.name == "Pickup0"){
    		newBook.transform.position = positions[0].transform.position;
    		isPlaced[0] = true;
    	}
    	else if (newBook.name == "Pickup1"){
    		newBook.transform.position = positions[1].transform.position;
    		isPlaced[1] = true;
    	}
    	else if (newBook.name == "Pickup2"){
    		newBook.transform.position = positions[2].transform.position;
    		isPlaced[2] = true;
    	}
    	else if (newBook.name == "Pickup3"){
    		newBook.transform.position = positions[3].transform.position;
    		isPlaced[3] = true;
    	}
    	else if (newBook.name == "Pickup4"){
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

    	Debug.Log("Its full now");
    		/*
			~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
				TODO: IMPLEMENT Sequence Afterwards
			~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
     		*/
    }
}
