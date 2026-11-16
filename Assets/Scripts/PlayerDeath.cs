using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

// add script to Player GameObject
// detects if user is on the ship - to carry out expected oxygen behaviour
// runs death method (once death conditions fulfilled) 
public class PlayerO2 : MonoBehaviour
{
    // flags
    public bool onShip;
    public bool oxygenEquipped;
    public bool wasKilled; // set by playerHealth script

    public Image suffocateVignette;
    private float oxygenDeathTimer = 0f;
    private float oxygenTimerMax = 8f;

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
            if (oxygenDeathTimer >= oxygenTimerMax) { StartCoroutine(Dead("You ran out of oxygen")); }
            else { oxygenDeathTimer += Time.deltaTime; }
           
        }
        else { oxygenDeathTimer = 0f; }

        updateVignetteAlpha();

        // player killed by creature
        if (wasKilled)
        {
            wasKilled = false;
            StartCoroutine(Dead("You were killed"));
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
    IEnumerator Dead(string message) 
    {
        // grab component to temporarily disable to allow teleportation
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false; 
        transform.position = Spawnpoint.transform.position; // teleport to spawn
        cc.enabled = true; // re-enable for correct movement 

        // display death message
        deathMessage.text = message;
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

    void updateVignetteAlpha()
    {
        Color vignetteColour = suffocateVignette.color;
        if (oxygenDeathTimer <= 0f) { vignetteColour.a = 0f; }
        else { vignetteColour.a = Mathf.Clamp01(oxygenDeathTimer / Mathf.Clamp(oxygenTimerMax,0f, oxygenTimerMax-3f)); }
        suffocateVignette.color = vignetteColour;
    }

}
