using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public int currentCapacity;
    public int totalCapacity;

    public GameObject player;
    private PlayerO2 playerScript;
    private bool onPlayer;
    public TextMeshProUGUI oxygenText;

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        currentCapacity = 30;
        totalCapacity = 30;
        timer = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        playerScript = player.GetComponent<PlayerO2>();
        onPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onPlayer && !playerScript.onShip) 
        {
            if (currentCapacity == 0)
            {
                onPlayer = false;
                playerScript.oxygenEquipped = false;
                Destroy(gameObject);

            }

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
            if (playerScript.O2Tank) 
            {
                Destroy(playerScript.O2Tank);
            }
            playerScript.oxygenEquipped = true;
            onPlayer = true;
            currentCapacity = totalCapacity;
            oxygenText.text = "Oxygen Capacity: 100%";
            gameObject.transform.position = new UnityEngine.Vector3(0, -10, 0);
            playerScript.O2Tank = gameObject;

        }
    }

}
