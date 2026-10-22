// Vague resource count - NOT USING - not attached to player - using resourceCollector
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public int resourceCount = 0;
    public TextMeshProUGUI resourceText;

    void Start()
    {
        UpdateResourceText();
    }

    public void AddResource()
    {
        resourceCount++;
        UpdateResourceText();
    }

    public bool UseResource(int amount)
    {
        if (resourceCount < amount)
        {
            return false;
        }

        resourceCount -= amount;
        UpdateResourceText();
        return true;
    }

    void UpdateResourceText()
    {
        if (resourceText != null)
        {
            resourceText.text = "Resources: " + resourceCount;
        }
    }
}
