
using UnityEngine;
using TMPro;

public class RepairPanel : MonoBehaviour
{
    public int repairCost = 3;

    public Renderer panelRenderer;
    public TextMeshProUGUI messageText;

    public GameObject winScreen;

    private ResourceManager resourceManager;
    private bool playerNearby;
    private bool repaired;

    public Color brokenColour = Color.red;
    public Color repairedColour = Color.green;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();

        if (panelRenderer == null)
        {
            panelRenderer = GetComponent<Renderer>();
        }

        panelRenderer.material.color = brokenColour;

        if (messageText != null)
        {
            messageText.text = "Panel broken";
        }

        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerNearby || repaired)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryRepair();
        }
    }

    void TryRepair()
    {
        if (resourceManager == null)
        {
            return;
        }

        if (resourceManager.UseResource(repairCost))
        {
            repaired = true;
            panelRenderer.material.color = repairedColour;

            if (messageText != null)
            {
                messageText.text = "PANEL REPAIRED!";
            }

            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }
        else
        {
            if (messageText != null)
            {
                messageText.text =
                    "Not enough resources! Need " + repairCost;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (!repaired && messageText != null)
            {
                messageText.text =
                    "Press E to repair (" + repairCost + " resources)";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (!repaired && messageText != null)
            {
                messageText.text = "Panel broken";
            }
        }
    }
}
