
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public Color Colour = Color.red;
    public Renderer panelRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResourceManager manager =
                FindObjectOfType<ResourceManager>();

            if (manager != null)
            {
                panelRenderer = GetComponent<Renderer>();
                panelRenderer.material.color = Colour;
                manager.AddResource();
                Destroy(gameObject);
            }
        }
    }
}
