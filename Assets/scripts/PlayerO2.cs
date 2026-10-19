using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// add script to Player GameObject
// for player specfic data of oxygen mechanic
// detects if user is on the ship - to carry out expected oxygen behaviour
public class PlayerO2 : MonoBehaviour
{
    private Vector3 playerSpawnpoint;
    public bool onShip;
    public bool oxygenEquipped;
    public GameObject O2Tank;
    public TextMeshProUGUI deathMessage;

    void Start()
    {
        // initialise
        playerSpawnpoint = new Vector3(0,5,0);
        onShip = true;
        oxygenEquipped = false;

    }

    void Update()
    {
        // player killed if outside without an O2 tank
        if (!onShip && !oxygenEquipped)
        {
            StartCoroutine(Dead());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship") 
        {
            onShip = true; 
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship") 
        {

            onShip = false;
        }

    }

    // player death and respawn
    IEnumerator Dead() 
    {
        // player death
        GetComponent<Renderer>().enabled = false;
        transform.position = playerSpawnpoint;
        deathMessage.text = "You ran out of oxygen";

        // player respawn
        yield return new WaitForSeconds(5);
        GetComponent<Renderer>().enabled = true;
        deathMessage.text = "";
    }
}
