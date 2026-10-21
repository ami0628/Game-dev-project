
using UnityEngine;
using TMPro;

public class ResourceCollector : MonoBehaviour
{
    public int resourceCount = 0;

    public TextMeshProUGUI resourceText;

    void Start()
    {
        UpdateResourceUI();
    }

    public void CollectResource()
    {
        resourceCount++;

        UpdateResourceUI();

        Debug.Log("Resource collected! Total: " + resourceCount);
    }
 
    public bool UseResource()
    {
        if (resourceCount > 0)
        {
            resourceCount--;

            UpdateResourceUI();

            Debug.Log("Resource used! Remaining: " + resourceCount);

            return true;
        }

        Debug.Log("Not enough resources!");
        return false;
    }

    void UpdateResourceUI()
    {
        if (resourceText != null)
        {
            resourceText.text = "Resources: " + resourceCount;
        }
    }


}
