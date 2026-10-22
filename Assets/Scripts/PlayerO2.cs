using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// add script to Player GameObject
// detects if user is on the ship - to carry out expected oxygen behaviour
public class PlayerO2 : MonoBehaviour
{
    // flags
    public bool onShip;
    public bool oxygenEquipped;

    public GameObject O2Tank; // current tank equipped (initially null)
    public GameObject Spawnpoint;

    // text components
    public TextMeshProUGUI deathMessage;
    public TextMeshProUGUI O2StarterMessage;
    public TextMeshProUGUI O2hint;

    void Start()
    {
        // initialise default
        onShip = true;
        oxygenEquipped = false;

        StartCoroutine(StartMessage());

    }

    void Update()
    {
        // player killed if outside without an O2 tank
        if (!onShip && !oxygenEquipped)
        {
            StartCoroutine(Dead());
        }
    }

    // set current location
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

        // grab component to temporarily disable to allow teleportation
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false; 
        transform.position = Spawnpoint.transform.position; // teleport to spawn
        cc.enabled = true; // re-enable for correct movement 

        // display death message
        deathMessage.text = "You ran out of oxygen";
        yield return new WaitForSeconds(5);
        deathMessage.text = "";

        
    }

    // display starter texts for 5 seconds each 
    IEnumerator StartMessage() 
    {
        O2StarterMessage.enabled = true;
        yield return new WaitForSeconds(5);
        O2StarterMessage.enabled = false;

        O2hint.enabled = true;
        yield return new WaitForSeconds(5);
        O2hint.enabled = false;
    }
}
