/*
 * Author: Jacie Thoo Yixuan
 * Date: 8/12/2024
 * Description: Handles the trigger area for the log in mission to start
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInTrigger : MonoBehaviour
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
            missionManager.PlayMissionDialogue("LogIn", false);
            triggerCollider.enabled = false; // Turn off collider so audio doesn't play again
        }
    }

    /// <summary>
    /// Marks "LogIn" mission as completed and plays audio from MissionManager
    /// Call in other script
    /// </summary>
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("LogIn");
        }
    }
}
