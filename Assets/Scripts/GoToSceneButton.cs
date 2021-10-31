using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToSceneButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
