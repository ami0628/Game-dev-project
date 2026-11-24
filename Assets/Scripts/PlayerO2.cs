using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// added to player GameObject
public class PlayerO2 : MonoBehaviour
{
    // components to control O2 UI
    private GameObject oxygenBar;
    private HealthBar oxygenScript;

    // capacities of O2 
    public float currentCapacity;
    public float totalCapacity;

    // script reference
    private PlayerDeath playerScript;

    // timer to accurately decrement oxygen each second
    private float timer;

    private GameObject tank;
    private bool playerNearby;
    public TextMeshProUGUI pickupText;

    void Start()
    {

        oxygenBar = GameObject.FindGameObjectWithTag("Oxygen Bar");
        playerScript = GetComponent<PlayerDeath>();
        oxygenScript = oxygenBar.GetComponent<HealthBar>();


        // setting default oxygen capacity
        // capacity represents seconds of usage
        totalCapacity = currentCapacity = timer = 30;
        currentCapacity = totalCapacity;

        // initialise UI bar - starts full
        oxygenScript.SetMaxHealth(totalCapacity);

        playerNearby = false;


    }

    void Update()
    {

        if (playerScript.currentScene == "OutsideEnv")
        {
            // sets O2 flag to false so playerScript runs method to respawn player
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

        if (playerNearby && Input.GetKeyDown(KeyCode.E)) 
        {
            Pickup();
        }
    }

    // refill oxygen when player picks up an O2 tank
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("O2")) 
        {
            tank = other.gameObject;
            playerNearby = true;
            pickupText.text = "Press E to pick up";

        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("O2"))
        {
            tank = null;
            playerNearby = false;
            pickupText.text = "";

        }
    }

    private void Pickup() 
    {
        if (tank != null) 
        {
            Destroy(tank);
        }
        pickupText.text = "";
        playerScript.hasOxygen = true;
        currentCapacity = totalCapacity;
        oxygenScript.SetMaxHealth(totalCapacity);
    }

}
