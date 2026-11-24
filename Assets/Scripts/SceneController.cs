using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    string currentScene;
    public GameObject spawnpoint;
    private Transform player;
    private GameObject tanks;
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

            // grab component to temporarily disable to allow teleportation
            CharacterController cc = other.GetComponent<CharacterController>();
            cc.enabled = false;

            player = other.transform;
            player.position = spawnpoint.transform.position;

            cc.enabled = true; // re-enable for correct movement 

            if (currentScene == "ShipEnv")
            {
                LoadOutside();
            }
            else if(currentScene == "OutsideEnv")
            {
                LoadShip();
            }

        }
    }
    
    // change scene to outside environment
    public void LoadOutside()
    {
        SceneManager.LoadScene(3);
        HideO2Tanks(false);
    }

    // change scene to ship environemnt
    public void LoadShip()
    {
        SceneManager.LoadScene(2);
        HideO2Tanks(true);
    }

    // hide/unhide O2 tanks in scene
    private void HideO2Tanks(bool setTo) 
    {
        // deactivate child objects to parent is always active and can be found
        tanks = GameObject.FindGameObjectWithTag("O2 Parent");
        foreach (Transform child in tanks.transform) 
        {
            if (child != null)
            {
                child.gameObject.SetActive(setTo);
            }
        }
    }

}
