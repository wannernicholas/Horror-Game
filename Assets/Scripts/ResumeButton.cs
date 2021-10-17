using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject manager;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Resume);
    }

    // Update is called once per frame
    void Resume()
    {
        manager.GetComponent<Manager>().UnPause();
    }
}
