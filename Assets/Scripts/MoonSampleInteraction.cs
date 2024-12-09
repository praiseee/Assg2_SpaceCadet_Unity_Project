/*
 * Author: Hoo Ying Qi Praise
 * Date: 7/12/2024
 * Description: Handles the Collection of Moon Samples Interaction
 */

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MoonSampleInteraction : MonoBehaviour
{
    public MissionManager missionManager;

    /// <summary>
    /// Cup's UI
    /// </summary>
    public TMP_Text CollectedText;

    /// <summary>
    /// Track samples collected
    /// </summary>
    private int moonSamplesCollected = 0;

    /// <summary>
    /// Track counted samples
    /// </summary>
    private HashSet<GameObject> countedSamples = new HashSet<GameObject>();

    private bool missionStarted = false;
    private bool missionCompleted = false;

    private void Start()
    {
        MoonText();
    }

    /// <summary>
    /// Moon Interaction when collected in a cup
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MoonSample"))
        {
            // Check if this moon sample has already been counted
            if (!countedSamples.Contains(collision.gameObject))
            {
                // Add the moon sample to the tracked set
                countedSamples.Add(collision.gameObject);

                // Handle moon sample collection
                moonSamplesCollected++;

                // Update the UI with the new count
                MoonText();
                Debug.Log($"Moon Sample collected! Total: {moonSamplesCollected}");

                // Check if the required number of samples is collected
                if (moonSamplesCollected == 6)
                {
                    MarkMissionAsCompleted(); // Mark the mission as completed
                }
            }
            else
            {
                Debug.Log("Moon Sample already counted.");
            }
        }
    }

    /// <summary>
    /// Text Display indicating no.of moon collected
    /// </summary>
    private void MoonText()
    {
        // Update the UI text to display the current number of moon samples collected
        CollectedText.text = $"Moon Samples: {moonSamplesCollected}/6";
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
    /// </summary>  
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("CollectSamples");
        }
    }
}
