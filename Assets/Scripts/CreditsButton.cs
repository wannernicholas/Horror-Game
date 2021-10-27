using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Credits;
    [SerializeField]
    private bool setActive;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Reload);
    }


    void Reload()
    {
    	//manager.GetComponent<Manager>().ReloadScene();
    	Credits.SetActive(setActive);
    }
}
