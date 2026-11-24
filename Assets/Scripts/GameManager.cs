using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to keep certain GameObjects between scenes
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

    // setting persistent objects in array
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

    // destroy any duplicates
    private void CleanUpAndDestroy() 
    {
        foreach (GameObject obj in persistentObjects) 
        {
            Destroy(obj);
        }

        Destroy(gameObject);
    }
}
