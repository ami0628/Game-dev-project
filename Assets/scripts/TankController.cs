using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// added to O2 Tank prefab
public class TankController : MonoBehaviour
{
    // components to control O2 UI
    private GameObject oxygenBar;
    private HealthBar oxygenScript;

    // capacities of O2 tanks
    public int currentCapacity;
    public int totalCapacity;

    // player object references
    public GameObject player;
    private PlayerO2 playerScript;
    private bool onPlayer;

    // timer to accurately decrement oxygen each second
    private float timer;


    void Start()
    {
       
        // must get components in script since script is assigned to prefab
        player = GameObject.FindGameObjectWithTag("Player");
        oxygenBar = GameObject.FindGameObjectWithTag("Oxygen Bar");
        playerScript = player.GetComponent<PlayerO2>();
        oxygenScript = oxygenBar.GetComponent<HealthBar>();


        // setting default tank capacities 
        // capacity represents seconds of usage
        totalCapacity = 30;
        currentCapacity = 30;

        // initialise UI bar - empty at first
        oxygenScript.SetMaxHealth(totalCapacity);
        oxygenScript.SetHealth(0, totalCapacity);

        timer = 0;

    }

    void Update()
    {
        if (onPlayer && !playerScript.onShip) 
        {
            // destroy tank if oxygen runs out
            // sets equipped to false so method to kill + respawn the player is run in playerScript
            if (currentCapacity == 0)
            {
        
                onPlayer = false;
                playerScript.oxygenEquipped = false;
                Destroy(gameObject);

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
        if (other.gameObject.tag == "Player") 
        {
            // if player already holds an O2 tank, destroy it to pick up new O2 tank
            // to remove old tank from the scene
            if (playerScript.O2Tank) 
            {
                Destroy(playerScript.O2Tank);
            }

            playerScript.oxygenEquipped = true;
            onPlayer = true;
            currentCapacity = totalCapacity;
            oxygenScript.SetMaxHealth(totalCapacity);

            // (instead of destroying) hide O2 tank after pick up to keep script running
            gameObject.transform.position = new UnityEngine.Vector3(0, -10, 0);
            playerScript.O2Tank = gameObject;

        }

    }

}
