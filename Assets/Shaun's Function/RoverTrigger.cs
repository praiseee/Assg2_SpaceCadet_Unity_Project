using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverTrigger : MonoBehaviour
{
    public Collider triggerCollider;
    public MissionManager missionManager;


    /// <summary>
    /// When player enters the trigger area
    /// play the audio to guide player to complete the mission
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionManager.PlayMissionDialogue("FixRover", false);
            triggerCollider.enabled = false; // Turn off collider so audio doesn't play again
        }
    }

    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("FixRover");
        }
    }
}
