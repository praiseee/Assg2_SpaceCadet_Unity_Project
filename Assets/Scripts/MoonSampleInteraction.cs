using UnityEngine;
using TMPro;

public class MoonSampleInteraction : MonoBehaviour
{
    public TextMesh collectedText; 
    private int moonSamplesCollected = 0;

    private void Start()
    {
        MoonText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoonSample"))
        {
            // Handle moon sample collection
            moonSamplesCollected++;
            MoonText();
            Debug.Log($"Moon Sample collected! Total: {moonSamplesCollected}");

            // Destroy the moon sample
            Destroy(other.gameObject);
        }
    }

    private void MoonText()
    {
        collectedText.text = $"Moon Samples: {moonSamplesCollected}";
    }
}
