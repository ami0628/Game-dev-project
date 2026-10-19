using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerO2 : MonoBehaviour
{
    private Vector3 playerSpawnpoint;
    public bool onShip;
    public bool oxygenEquipped;
    public GameObject O2Tank;
    public TextMeshProUGUI deathMessage;

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnpoint = new Vector3(0,5,0);
        onShip = true;
        oxygenEquipped = false;

    }

    // Update is called once per frame
    void Update()
    {
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

    IEnumerator Dead() 
    {
        GetComponent<Renderer>().enabled = false;
        transform.position = playerSpawnpoint;
        deathMessage.text = "You ran out of oyxgen";

        yield return new WaitForSeconds(5);
        GetComponent<Renderer>().enabled = true;
        deathMessage.text = "";
    }
}
