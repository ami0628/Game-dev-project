using UnityEngine;

// Destroy the resources upon collision - removed from screens
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