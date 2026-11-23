using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] persistentObjects;
   
    private void Awake()
    {
        if (instance != null)
        {
            CleanUpAndDestroy();
            return;
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            PersistentObjects();
        }
    }

    private void PersistentObjects() 
    {
        foreach (GameObject obj in persistentObjects) 
        {
            if (obj != null) 
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy() 
    {
        foreach (GameObject obj in persistentObjects) 
        {
            Destroy(obj);
        }

        Destroy(gameObject);
    }
}
