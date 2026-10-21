
using UnityEngine;
using TMPro;

public class RepairPanel : MonoBehaviour
{
    public ResourceCollector playerResources;
    public Renderer panelRenderer;
    public TextMeshProUGUI interactionText;
    public GameObject winPanel;

    public Color brokenColor = Color.red;
    public Color repairedColor = Color.green;

    private bool playerNearby = false;
    private bool isRepaired = false;

    void Start()
    {
        if (panelRenderer != null)
        {
            panelRenderer.material.color = brokenColor;
        }
        else
        {
            Debug.LogError("RepairPanel: Panel Renderer is not assigned!");
        }

        if (playerResources == null)
        {
            Debug.LogError("RepairPanel: Player Resources is not assigned!");
        }

        if (winPanel != null)
            winPanel.SetActive(false);

        if (interactionText != null)
            interactionText.text = "";

        Debug.Log("RepairPanel script started.");
    }

    void Update()
    {
        if (!playerNearby || isRepaired)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed near repair panel.");
            Repair();
        }
    }

    void Repair()
        
    {
        if (playerResources == null)
        {
            Debug.LogError(
                "RepairPanel: Player Resources is not assigned!"
            );
            return;
        }

        if (!playerResources.UseResource())
        {
            if (interactionText != null)
                interactionText.text =
                    "You need a resource to repair the ship!";

            Debug.Log("Repair failed: no resources.");
            return;
        }

        isRepaired = true;

        if (panelRenderer != null)
            panelRenderer.material.color = repairedColor;
        else
            Debug.LogWarning("Panel Renderer is not assigned.");

        if (interactionText != null)
            interactionText.text = "Ship repaired!";

        if (winPanel != null)
            winPanel.SetActive(true);
        else
            Debug.LogWarning("Win Panel is not assigned.");

        Debug.Log("SUCCESS: Ship repaired!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Repair zone entered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player is near the repair panel.");

            if (interactionText != null && !isRepaired)
                interactionText.text =
                    "Press E to repair the ship";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactionText != null && !isRepaired)
                interactionText.text = "";

            Debug.Log("Player left the repair zone.");
        }
    }
}
