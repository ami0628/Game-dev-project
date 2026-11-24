using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    string currentScene;
    public GameObject spawnpoint;
    private Transform player;
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


            if (currentScene == "ShipEnv")
            {
                SceneManager.LoadScene(3);
            }
            else if(currentScene == "OutsideEnv")
            {
                SceneManager.LoadScene(2);
            }

            // grab component to temporarily disable to allow teleportation
            CharacterController cc = other.GetComponent<CharacterController>();
            cc.enabled = false;

            player = other.transform;
            player.position = spawnpoint.transform.position;

            cc.enabled = true; // re-enable for correct movement 
        }
    }

}
