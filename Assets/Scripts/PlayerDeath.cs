using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// add script to Player GameObject
// detects if user is on the ship - to carry out expected oxygen behaviour
// runs death method (once death conditions fulfilled) 
public class PlayerO2 : MonoBehaviour
{
    // flags
    public bool oxygenEquipped;
    public bool wasKilled; // set by playerHealth script

    public string currentScene;
    public GameObject O2Tank; // current tank equipped (initially null)
    public GameObject Spawnpoint;

    // text components
    public TextMeshProUGUI deathMessage;
    public TextMeshProUGUI O2StarterMessage;
    public TextMeshProUGUI O2hint;

    void Start()
    {
        // initialise default
        currentScene = SceneManager.GetActiveScene().name;
        oxygenEquipped = false;

        StartCoroutine(StartMessage());

    }

    void Update()
    {
        // player killed if outside without an O2 tank
        if (currentScene == "OutsideEnv" && !oxygenEquipped)
        {
            StartCoroutine(Dead("You ran out of oxygen"));
        }

        // player killed by creature
        if (wasKilled) 
        {
            wasKilled = false;
            StartCoroutine(Dead("You were killed"));
        }
    }

    // player death and respawn
    IEnumerator Dead(string message) 
    {
        // grab component to temporarily disable to allow teleportation
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false;
        SceneManager.LoadScene(2); // teleport to spawn

        // display death message
        deathMessage.text = message;
        yield return new WaitForSeconds(5);
        deathMessage.text = "";
        cc.enabled = true; // re-enable for correct movement


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
