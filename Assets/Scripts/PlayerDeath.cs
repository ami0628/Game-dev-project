using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

// add script to Player GameObject
// detects if user is on the ship - to carry out expected oxygen behaviour
// runs death method (once death conditions fulfilled) 
public class PlayerDeath : MonoBehaviour
{
    // flags
    public bool hasOxygen;
    public bool wasKilled; // set by playerHealth script

    public Image suffocateVignette;
    private float oxygenDeathTimer = 0f;
    private float oxygenTimerMax = 8f;

    public string currentScene;

    // text components
    public TextMeshProUGUI deathMessage;
    public TextMeshProUGUI O2hint;

    public GameObject teleporter;
    private SceneController sceneScript;

    void Start()
    {
        // initialise default
        currentScene = SceneManager.GetActiveScene().name;
        hasOxygen = true;
        sceneScript = teleporter.GetComponent<SceneController>();

        StartCoroutine(StartMessage());

    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        // player killed if outside if oxygen is empty
        if (currentScene == "OutsideEnv" && !hasOxygen)
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

    // player death and respawn
    IEnumerator Dead(string message) 
    {
        // temporarily disable character controller to allow teleportation
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;

        // change scene + teleport player
        sceneScript.LoadShip();
        transform.position = new Vector3(0, 5, -20);
        cc.enabled = true; // re-enable for correct movement

        // display death message
        deathMessage.text = message;
        yield return new WaitForSeconds(5);
        deathMessage.text = "";


    }

    // display starter texts for 5 seconds
    IEnumerator StartMessage() 
    {

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
