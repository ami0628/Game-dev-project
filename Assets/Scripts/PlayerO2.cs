using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// added to O2 Tank prefab
public class PlayerO2 : MonoBehaviour
{
    // components to control O2 UI
    private GameObject oxygenBar;
    private HealthBar oxygenScript;

    // capacities of O2 tanks
    public float currentCapacity;
    public float totalCapacity;

    // player object references
    public GameObject player;
    private PlayerDeath playerScript;

    // timer to accurately decrement oxygen each second
    private float timer;


    void Start()
    {

        oxygenBar = GameObject.FindGameObjectWithTag("Oxygen Bar");
        playerScript = GetComponent<PlayerDeath>();
        oxygenScript = oxygenBar.GetComponent<HealthBar>();


        // setting default tank capacities 
        // capacity represents seconds of usage
        totalCapacity = currentCapacity = timer = 30;
        currentCapacity = totalCapacity;

        // initialise UI bar - starts full
        oxygenScript.SetMaxHealth(totalCapacity);


    }

    void Update()
    {
        if (playerScript.currentScene == "OutsideEnv") 
        {
            // destroy tank if oxygen runs out
            // sets equipped to false so method to kill + respawn the player is run in playerScript
            if (currentCapacity == 0)
            {
        
                playerScript.hasOxygen = false;

            }

            // decrease oxygen level every second
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                currentCapacity--;
                oxygenScript.SetHealth(currentCapacity, totalCapacity);
                timer = 0;
            }

        }
    }

    // method called when player picks up an O2 tank
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "O2") 
        {
            
            Destroy(other.gameObject);

            playerScript.hasOxygen = true;
            currentCapacity = totalCapacity;
            oxygenScript.SetMaxHealth(totalCapacity);

        }

    }

}
