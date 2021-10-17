using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadLevelButton : MonoBehaviour
{

	[SerializeField]
    private GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Reload);
    }


    void Reload()
    {
    	manager.GetComponent<Manager>().ReloadScene();
    }
    
}
