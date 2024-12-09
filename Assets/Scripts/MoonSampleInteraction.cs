using UnityEngine;
using TMPro;

public class MoonSampleInteraction : MonoBehaviour
{
    public TMP_Text CollectedText; // For the cup's UI
    private int moonSamplesCollected = 0; // Track samples collected

    private bool missionStarted = false;
    private bool missionCompleted = false;

    public MissionManager missionManager;

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

    /// <summary>
    /// Play start mission dialogue when the cup is held
    /// </summary>
    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            missionManager.PlayMissionDialogue("CollectSamples", false);
        }
    }

    /// <summary>
    /// Marks "CollectSamples" mission as completed and plays audio from MissionManager
    /// Call when the mission conditions are met (e.g. 10 samples collected)
    /// </summary>
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("CollectSamples");
            missionCompleted = true;
        }
    }
}
