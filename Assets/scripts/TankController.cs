using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class TankController : MonoBehaviour
{
    // capacities of O2 tanks
    public int currentCapacity;
    public int totalCapacity;

    // player references
    public GameObject player;
    private PlayerO2 playerScript;

    private bool onPlayer;
    public TextMeshProUGUI oxygenText;

    private float timer;


    void Start()
    {
        currentCapacity = 30;
        totalCapacity = 30;
        timer = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        playerScript = player.GetComponent<PlayerO2>();
        onPlayer = false;
    }

    void Update()
    {
        if (onPlayer && !playerScript.onShip) 
        {
            // destroy tank if oxygen runs out
            // sets equipped to false so method to kill player is run in playerScript
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
                int oxygenPercent = (int)Math.Round((double)(100 * currentCapacity) / totalCapacity);

                oxygenText.text = "Oxygen Capacity: " + oxygenPercent + "%";
                timer = 0;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            // if player already holds an O2 tank
            // destroy it to pick up new O2 tank
            if (playerScript.O2Tank) 
            {
                Destroy(playerScript.O2Tank);
            }

            playerScript.oxygenEquipped = true;
            onPlayer = true;
            currentCapacity = totalCapacity;
            oxygenText.text = "Oxygen Capacity: 100%";

            // hide O2 tank after pick up to keep script running
            gameObject.transform.position = new UnityEngine.Vector3(0, -10, 0);
            playerScript.O2Tank = gameObject;

        }
    }

}
