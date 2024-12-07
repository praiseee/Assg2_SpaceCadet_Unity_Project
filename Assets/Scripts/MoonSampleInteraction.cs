using UnityEngine;
using TMPro;

public class MoonSampleInteraction : MonoBehaviour
{
    public TMP_Text CollectedText; // For the cup's UI
    private int moonSamplesCollected = 0; // Track samples collected

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
        CollectedText.text = $"Moon Samples: {moonSamplesCollected}";
    }
}
