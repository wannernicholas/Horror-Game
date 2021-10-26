using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNode : MonoBehaviour
{
    public List<GameObject> connectedNodes;
    void Start()
    {
    	foreach (Transform child in transform)
    		child.gameObject.SetActive(false);
    }
    
}
