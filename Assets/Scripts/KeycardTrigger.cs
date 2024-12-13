/*
 * Author: Jacie Thoo Yixuan
 * Date: 7/12/2024
 * Description: Handles keycard access interaction
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardTrigger : MonoBehaviour
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
            missionManager.PlayMissionDialogue("Keycard", false);
            triggerCollider.enabled = false; // Turn off collider so audio doesn't play again
        }
    }

    /// <summary>
    /// Marks "Keycard" mission as completed and plays audio from MissionManager
    /// Call in DoorAccessPanel script
    /// </summary>
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("Keycard");
        }
    }
}
