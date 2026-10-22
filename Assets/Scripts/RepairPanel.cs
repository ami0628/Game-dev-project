// Repair panel mechanics
using UnityEngine;
using TMPro;

public class RepairPanel : MonoBehaviour
{
    // Needs to keep track of resources + interactions
    public ResourceCollector playerResources;
    public Renderer panelRenderer;
    public TextMeshProUGUI interactionText;
    public GameObject winPanel;

    public int resourcesRequired = 3;
    public Color brokenColor = Color.red;
    public Color repairedColor = Color.green;

    private bool playerNearby = false;
    private bool isRepaired = false;

    void Start()
    {
        // initial start up + unity debugging
        if (panelRenderer != null)
            panelRenderer.material.color = brokenColor;
        else
            Debug.LogError("RepairPanel: Panel Renderer is not assigned!");

        if (playerResources == null)
            Debug.LogError("RepairPanel: Player Resources is not assigned!");

        if (winPanel != null)
            winPanel.SetActive(false);

        SetMessage("");
    }

    void Update()
    {
        if (!playerNearby || isRepaired)
            return;

        // Keep the prompt in sync with the current resource count
        RefreshPrompt();

        if (Input.GetKeyDown(KeyCode.E))
            Repair();
    }

    void RefreshPrompt()
    {
        if (playerResources.resourceCount >= resourcesRequired)
            SetMessage("Press E to repair the ship");
        else
            SetMessage("You need " + resourcesRequired + " resources to repair the ship ("
                       + playerResources.resourceCount + "/" + resourcesRequired + ")");
    }

    void Repair()
    {
        if (playerResources.resourceCount < resourcesRequired)
        {
            Debug.Log("Repair failed: not enough resources.");
            return;
        }

        // Spend all required resources
        for (int i = 0; i < resourcesRequired; i++)
            playerResources.UseResource();

        isRepaired = true;

        if (panelRenderer != null)
            panelRenderer.material.color = repairedColor;

        SetMessage("");

        if (winPanel != null)
            winPanel.SetActive(true);

        Debug.Log("SUCCESS: Ship repaired!");
    }


    // defines the interaction text based on events 
    void SetMessage(string message)
    {
        if (interactionText != null)
            interactionText.text = message;
    }


    // can only repair if player is within vicinity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (!isRepaired) RefreshPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (!isRepaired) SetMessage("");
        }
    }
}