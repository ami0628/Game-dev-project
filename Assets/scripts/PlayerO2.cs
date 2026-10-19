using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerO2 : MonoBehaviour
{
    private Vector3 playerSpawnpoint;
    public TextMeshProUGUI posText;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnpoint = new Vector3(0, 0, 5);
        posText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship") 
        {
            posText.text = "location: ship";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship") 
        {
            posText.text = "location: outside";
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead() 
    {
        Debug.Log("dead");
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(5);
        Debug.Log("respawn");
        transform.position = playerSpawnpoint;
        GetComponent<Renderer>().enabled = true;
    }
}
