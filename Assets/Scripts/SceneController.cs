using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    string currentScene;
    public void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void onStartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(currentScene == "ShipEnv")
            {
                SceneManager.LoadScene(3);
            }
            else if(currentScene == "OutsideEnv")
            {
                SceneManager.LoadScene(2);
            }
        }
    }

}
