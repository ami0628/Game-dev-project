using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void onStartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
    }
}
