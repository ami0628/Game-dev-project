
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the resource: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            ResourceCollector collector =
                other.GetComponent<ResourceCollector>();

            if (collector != null)
            {
                collector.CollectResource();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError(
                    "ResourceCollector not found on the Player!"
                );
            }
        }
    }
}
