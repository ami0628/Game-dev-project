using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}